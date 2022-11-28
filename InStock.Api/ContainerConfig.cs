using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Services.Mappers;

namespace InStock.Api
{
    public static class ContainerConfig
    {
        public static void Configure(IHostBuilder host)
        {
            host.ConfigureServices((hostContext, services) =>
            {
                //Dependency injection below
                //services.AddSingleton<IAppSettingsService, AppSettingsService>();
                //TODO: Find a way to automate this bootstrapping per mapper and repo
                services.AddTransient<IRepository<EarningsEntity>, EarningsRepository>();
                services.AddTransient<IMapper<IEarnings, EarningsEntity, EarningsModel>, EarningsMapper>();
                services.AddTransient<IMapper<IEarnings, EarningsEntity, EarningsModel>, EarningsMapper>();
            });
        }
    }

    //https://stackoverflow.com/questions/39174989/how-to-register-multiple-implementations-of-the-same-interface-in-asp-net-core
    //https://github.com/aspnet/DependencyInjection/blob/release/2.1/src/DI.Abstractions/ServiceProviderServiceExtensions.cs#L98-L118
    //private static void AddTransient<IServiceInterface>(Assembly assembly)
    //{
    //    Assembly.GetEntryAssembly().GetTypesAssignableFrom<IService>().ForEach((t) =>
    //    {
    //        services.AddScoped(typeof(IService), t);
    //    });
    //}
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
