using InStock.Lib.DataAccess;
using InStock.Lib.Services.Mappers;
using System.Reflection;

namespace InStock.Api
{
    public static class ContainerConfig
    {
        public static void Configure(IHostBuilder host)
        {
            host.ConfigureServices((hostContext, services) =>
            {
                var asm = Assembly.Load("InStock.Lib");
                var types = asm.GetTypes();

                AddRepositories(services, types,
                    "Earnings",
                    "Position",
                    "Quote",
                    "Stock",
                    "Trade",
                    "User");

                //Dependency injection below
                //services.AddSingleton<IAppSettingsService, AppSettingsService>();
                //TODO: Find a way to automate this bootstrapping per mapper and repo
                
            });
        }

        //https://stackoverflow.com/questions/39174989/how-to-register-multiple-implementations-of-the-same-interface-in-asp-net-core
        //https://github.com/aspnet/DependencyInjection/blob/release/2.1/src/DI.Abstractions/ServiceProviderServiceExtensions.cs#L98-L118
        private static void AddRepositories(IServiceCollection services, Type[] types, params string[] classRoots)
        {
            var ns = "InStock.Lib";
            var iRepo = typeof(IRepository<>);
            var iMap = typeof(IMapper<,,>);

            foreach (var root in classRoots)
            {
                var iShared = types.Single(x => x.FullName == $"{ns}.Entities.I{root}");
                var entity = types.Single(x => x.FullName == $"{ns}.Entities.{root}Entity");
                var model = types.Single(x => x.FullName == $"{ns}.Models.{root}Model");
                var repository = types.Single(x => x.FullName == $"{ns}.DataAccess.{root}Repository");
                var mapper = types.Single(x => x.FullName == $"{ns}.Services.Mappers.{root}Mapper");

                //Set interface generic types
                var iRepoRoot = iRepo.MakeGenericType(entity);
                var iMapRoot = iMap.MakeGenericType(iShared, entity, model);

                //Written out example
                //services.AddTransient<IRepository<EarningsEntity>, EarningsRepository>();
                //services.AddTransient<IMapper<IEarnings, EarningsEntity, EarningsModel>, EarningsMapper>();

                //Reflection equivalent
                services.AddScoped(iRepoRoot, repository);
                services.AddScoped(iMapRoot, mapper);
            }
        }
    }
}

// Keep this for later when I need to add in Serilog.
/*
 .UseSerilog((hostContext, serviceProvider, LoggerConfiguration) =>
            {
                var profile = serviceProvider.GetService<IProfile>();

                var path = profile.Audit.Path + "\\Log.log";

                //Since this is configured here, don't do it in the JSON also otherwise the logging will appear twice
                LoggerConfiguration
                    .ReadFrom.Configuration(hostContext.Configuration)
                    .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                    .WriteTo.Console();

                if (hostContext.HostingEnvironment.IsDevelopment())
                {
                    LoggerConfiguration.WriteTo.Seq("http://localhost:5341");
                }
            })
 */
