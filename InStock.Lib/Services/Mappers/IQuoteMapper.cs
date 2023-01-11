using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public interface IQuoteMapper
    {
        QuoteV1CreatedModel ToCreatedModel(IQuote target);
        QuoteEntity ToEntity(IQuote target);
        QuoteEntity ToEntity(QuoteModel model);
        QuoteModel ToModel(IQuote target);
        QuoteModel ToModel(QuoteEntity entity);
    }
}