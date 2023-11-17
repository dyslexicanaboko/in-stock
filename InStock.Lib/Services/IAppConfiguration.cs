namespace InStock.Lib.Services;

public interface IAppConfiguration
{
    string GetConnectionString();

    int QuoteCacheWindowMinutes { get; }
}