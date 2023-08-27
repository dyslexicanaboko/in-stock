using System.Reflection;
using InStock.Lib.DataAccess;
using InStock.Lib.Services;
using InStock.Lib.Services.ApiClient;
using Serilog;

namespace InStock.Api
{
  public static class ContainerConfig
  {
    private const string Ns = "InStock.Lib";

    public static void Configure(IHostBuilder host)
    {
      host.ConfigureServices(
          (_, services) =>
          {
            services.AddSingleton<IAppConfiguration, AppConfiguration>();
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddSingleton<IStockQuoteApiService, StockQuoteApiService>();

            var asm = Assembly.Load("InStock.Lib");
            var types = asm.GetTypes();

            RegisterPipelines(
              services,
              types,
              "Earnings",
              "Position",
              "Quote",
              "Stock",
              "Trade",
              "User");

            services.AddScoped<IPositionCalculator, PositionCalculator>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ITokenService, TokenService>();

            //Dependency injection below
            //services.AddSingleton<IAppSettingsService, AppSettingsService>();
            //TODO: Find a way to automate this bootstrapping per mapper and repo
          })
        .UseSerilog(
          (hostContext, loggerConfiguration) =>
          {
            //TODO: Make this configurable
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Log.log");

            //Since this is configured here, don't do it in the JSON also otherwise the logging will appear twice
            loggerConfiguration
              .ReadFrom.Configuration(hostContext.Configuration)
              .WriteTo.File(path, rollingInterval: RollingInterval.Day);

            if (hostContext.HostingEnvironment.IsDevelopment())
              loggerConfiguration.WriteTo.Seq("http://localhost:5341");
          });
    }

    //https://stackoverflow.com/questions/39174989/how-to-register-multiple-implementations-of-the-same-interface-in-asp-net-core
    //https://github.com/aspnet/DependencyInjection/blob/release/2.1/src/DI.Abstractions/ServiceProviderServiceExtensions.cs#L98-L118
    private static void RegisterPipelines(IServiceCollection services, Type[] types, params string[] classRoots)
    {
      var iRepo = typeof(IRepository<>);

      //var iMap = typeof(IMapper<,,>);

      foreach (var root in classRoots)
      {
        //var iShared = types.Single(x => x.FullName == $"{ns}.Entities.I{root}");
        var entity = types.Single(x => x.FullName == $"{Ns}.Entities.{root}Entity");

        //var model = types.Single(x => x.FullName == $"{ns}.Models.{root}Model");
        var iRepository = types.Single(x => x.FullName == $"{Ns}.DataAccess.I{root}Repository");
        var repository = types.Single(x => x.FullName == $"{Ns}.DataAccess.{root}Repository");
        var mapper = types.Single(x => x.FullName == $"{Ns}.Services.Mappers.{root}Mapper");
        var iMapper = types.SingleOrDefault(x => x.FullName == $"{Ns}.Services.Mappers.I{root}Mapper");
        var iService = types.SingleOrDefault(x => x.FullName == $"{Ns}.Services.I{root}Service");
        var service = types.SingleOrDefault(x => x.FullName == $"{Ns}.Services.{root}Service");
        var iValidation = types.SingleOrDefault(x => x.FullName == $"{Ns}.Validation.I{root}Validation");
        var validation = types.SingleOrDefault(x => x.FullName == $"{Ns}.Validation.{root}Validation");

        //Set interface generic types
        var iRepoRoot = iRepo.MakeGenericType(entity);

        //Written out example
        //services.AddTransient<IRepository<EarningsEntity>, EarningsRepository>();
        //services.AddTransient<IMapper<IEarnings, EarningsEntity, EarningsModel>, EarningsMapper>();

        //Reflection equivalent
        services.AddScoped(iRepository, repository);
        services.AddScoped(iRepoRoot, repository);

        //Temporarily skipping trade until I can get rid of the other mappers I don't want anymore
        //if (root != "Trade")
        //{
        //    var iMapRoot = iMap.MakeGenericType(iShared, entity, model);

        //    services.AddScoped(iMapRoot, mapper);
        //}

        RegisterAsScoped(services, iMapper, mapper);

        RegisterAsScoped(services, iService, service);

        RegisterAsScoped(services, iValidation, validation);
      }
    }

    private static void RegisterAsScoped(IServiceCollection services, Type? interfaceType, Type? implementationType)
    {
      if (interfaceType == null || implementationType == null) return;

      services.AddScoped(interfaceType, implementationType);
    }
  }
}
