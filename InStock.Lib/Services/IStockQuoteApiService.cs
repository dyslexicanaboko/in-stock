using InStock.Lib.Models;

namespace InStock.Lib.Services
{
    public interface IStockQuoteApiService
    {
        StockQuoteModel GetQuote(string symbol);
    }
}
