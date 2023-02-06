using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface IQuoteRepository
        : IRepository<QuoteEntity>
    {
        QuoteEntity? Select(string symbol);

        QuoteEntity? SelectRecent(string symbol, DateTime relativeTimeUtc, int lastXMinutes);
    }
}
