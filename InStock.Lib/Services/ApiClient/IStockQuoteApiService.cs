namespace InStock.Lib.Services.ApiClient
{
    public interface IStockQuoteApiService
    {
        Task<StockQuoteModel> GetQuote(string symbol);
    }
}
