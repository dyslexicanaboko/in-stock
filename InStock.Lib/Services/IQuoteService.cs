using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public interface IQuoteService
    {
        Task<QuoteEntity> Add(string symbol);
        
        Task<QuoteEntity> Add(int stockId, string symbol);
        
        QuoteEntity? GetQuote(int quoteId);
        
        QuoteEntity? GetQuote(string symbol);

        QuoteEntity? GetRecentQuote(string symbol);
    }
}