using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public interface IQuoteService
    {
        Task<QuoteEntity> Add(string symbol);
        
        Task<QuoteEntity> Add(StockEntity stock);
        
        QuoteEntity? GetQuote(int id);
        
        QuoteEntity? GetQuote(string symbol);

        QuoteEntity? GetRecentQuote(string symbol);
    }
}