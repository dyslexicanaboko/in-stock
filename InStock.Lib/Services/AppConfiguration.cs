using Microsoft.Extensions.Configuration;

namespace InStock.Lib.Services
{
    public class AppConfiguration : IAppConfiguration
    {
        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("InStock");

            return connectionString!;
        }
    }
}
