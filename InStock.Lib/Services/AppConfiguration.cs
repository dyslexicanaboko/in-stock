using Microsoft.Extensions.Configuration;

namespace InStock.Lib.Services
{
  public class AppConfiguration
    : IAppConfiguration
  {
    private readonly IConfiguration _configuration;

    public AppConfiguration(IConfiguration configuration) => _configuration = configuration;

    public string GetConnectionString()
    {
      var connectionString = _configuration.GetConnectionString("InStock");

      return connectionString!;
    }

    /// <inheritdoc />
    public int QuoteCacheWindowMinutes => _configuration.GetValue("QuoteCacheWindowMinutes", defaultValue: 5);
  }
}
