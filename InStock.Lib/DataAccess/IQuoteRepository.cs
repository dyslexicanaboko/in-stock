using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface IQuoteRepository
        : IRepository<QuoteEntity>
    {
        QuoteEntity? Select(string symbol);
    }
}
