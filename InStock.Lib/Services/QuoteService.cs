using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services.ApiClient;
using InStock.Lib.Validation;

namespace InStock.Lib.Services
{
  public class QuoteService : IQuoteService
  {
    private readonly IQuoteRepository _repoQuote;

    private readonly IStockRepository _repoStock;

    private readonly IStockQuoteApiService _stockQuoteApi;

    //TODO: Proper centralized cache needed to stop abuse
    //Temporary solution until a better one is found (Redis?)
    private readonly Dictionary<string, QuoteEntity> _quoteCache;

    public QuoteService(
      IQuoteRepository repoQuote,
      IStockRepository repoStock,
      IStockQuoteApiService stockQuoteApi)
    {
      _repoQuote = repoQuote;

      _repoStock = repoStock;

      _stockQuoteApi = stockQuoteApi;

      _quoteCache = new Dictionary<string, QuoteEntity>(StringComparer.InvariantCultureIgnoreCase);
    }

    public QuoteEntity? GetQuote(int quoteId)
    {
      Validations.IsGreaterThanZero(quoteId, nameof(quoteId));

      var dbEntity = _repoQuote.Using(x => x.Select(quoteId));

      return dbEntity;
    }

    public QuoteEntity? GetQuote(string symbol)
    {
      Validations.IsSymbolValid(symbol);

      var dbEntity = _repoQuote.Using(x => x.Select(symbol));

      return dbEntity;
    }

    public QuoteEntity? GetRecentQuote(string symbol)
    {
      Validations.IsSymbolValid(symbol);

      if (_quoteCache.TryGetValue(symbol, out var cached)) return cached;

      //This is hard coded for now until I figure out how to deal with it properly
      var dbEntity = _repoQuote.Using(x => x.SelectRecent(symbol, DateTime.UtcNow, 5));

      return dbEntity;
    }

    public async Task<QuoteEntity> Add(string symbol)
    {
      Validations.IsSymbolValid(symbol);

      var stock = _repoStock.Using(x => x.Select(symbol));

      if (stock == null) throw NotFound.Stock(symbol);

      return await Add(stock.StockId, symbol);
    }

    public async Task<QuoteEntity> Add(int stockId, string symbol)
    {
      var existingQuote = GetRecentQuote(symbol);

      if (existingQuote != null) return existingQuote;

      var stockQuote = await _stockQuoteApi.GetQuote(symbol);

      //If the quote cannot be found raise exception I suppose?
      if (stockQuote == null) throw NotFound.Symbol(symbol);

      var quote = new QuoteEntity
      {
        StockId = stockId,
        Date = stockQuote.Date,
        Price = stockQuote.Price,
        Volume = stockQuote.Volume
      };

      quote.QuoteId = _repoQuote.Using(x => x.Insert(quote));

      _quoteCache.TryAdd(symbol, quote);

      return quote;
    }
  }
}
