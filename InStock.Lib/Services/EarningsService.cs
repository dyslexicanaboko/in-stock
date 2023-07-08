using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;
using InStock.Lib.Services.Mappers;
using InStock.Lib.Validation;

namespace InStock.Lib.Services
{
  public class EarningsService : IEarningsService
    {
        public const int MaxEntries = 4;
        private readonly IEarningsRepository _repoEarnings;
        private readonly IStockRepository _repoStock;
        private readonly IEarningsMapper _mapper;

        public EarningsService(
            IEarningsRepository repoEarnings,
            IStockRepository repoStock,
            IEarningsMapper mapper)
        {
            _repoEarnings = repoEarnings;
         
            _repoStock = repoStock;

            _mapper = mapper;
        }

        public EarningsEntity? GetEarnings(int id)
        {
            var dbEntity = _repoEarnings.Using(x => x.Select(id));

            return dbEntity;
        }

        public IList<EarningsEntity> GetEarnings(string symbol)
        {
            Validations.IsSymbolValid(symbol);

            if (_repoStock.Using(x => x.Select(symbol)) is null) throw NotFound.Symbol(symbol);

            var dbEntity = _repoEarnings.Using(x => x.Select(symbol));

            return dbEntity;
        }

        public IList<AddEarningsResult> Add(EarningsEntity earnings)
            => Add(new List<EarningsEntity> { earnings });

        public IList<AddEarningsResult> Add(IList<EarningsEntity>? multipleEarnings)
        {
            Validations.IsNotNull(multipleEarnings, nameof(multipleEarnings));

            var stockIds = multipleEarnings.Select(x => x.StockId).Distinct().ToList();

            //Get all stocks to prove they exist and order them by Stock Id so that the positions
            //are processed in stock order (grouped by stockId)
            var stocks = _repoStock
                .Using(x => x.Select(stockIds))
                .OrderBy(x => x.StockId)
                .ToList();

            var multipleEarningsSorted = multipleEarnings.OrderBy(x => x.StockId).ToList();

            //Years are meaningless here, so they will be minified on purpose.
            multipleEarningsSorted.ForEach(x => x.Date = GetYearAgnosticDate(x.Date));
            
            var results = new List<AddEarningsResult>(multipleEarningsSorted.Count);
            var dictEarnings = new Dictionary<int, List<EarningsEntity>>();

            using (_repoEarnings)
            {
                foreach (var e in multipleEarningsSorted)
                {
                    var r = new AddEarningsResult(e);

                    try
                    {
                        var s = stocks.SingleOrDefault(x => x.StockId == e.StockId);

                        //Stock must exist before attempting to make positions with it
                        if (s == null)
                            throw NotFound.Stock(e.StockId);

                        //To enforce uniqueness, all earnings must be returned so a compare can be performed
                        List<EarningsEntity> lstEarnings;

                        //To avoid repeated trips to the database, use memoization to for the earnings for each stock invovled
                        if (dictEarnings.ContainsKey(e.StockId))
                        {
                            lstEarnings = dictEarnings[e.StockId];
                        }
                        else
                        {
                            lstEarnings = _repoEarnings.SelectAll(e.StockId).ToList();
                            
                            dictEarnings.Add(e.StockId, lstEarnings);
                        }

                        //Enforce only four entries
                        if (lstEarnings.Count == MaxEntries)
                            throw new MaxEntriesException(s.Symbol, "earnings", MaxEntries);

                        //Reject duplicate entries
                        CheckForDuplicates(lstEarnings, e, s.Symbol);

                        //Order cannot be enforced because it depends on the user to input the dates and order correctly.
                        e.EarningsId = _repoEarnings.Insert(e);

                        //Update the cache
                        lstEarnings.Add(e);

                        results.Add(r);
                    }
                    catch (Exception ex)
                    {
                        r.Failure(ex);
                    }

                    results.Add(r);
                }
            }

            return results;
        }

        public void Edit(EarningsEntity? earnings)
        {
            Validations.IsNotNull(earnings, nameof(earnings));

            //Years are meaningless here, so they will be minified on purpose.
            earnings!.Date = GetYearAgnosticDate(earnings.Date);

            //Not allowing the user to change the stock the earnings is associated with, therefore this must exist
            var stock = _repoStock.Using(x => x.Select(earnings.StockId));

            using (_repoEarnings)
            {
                //To enforce uniqueness, all earnings must be returned so a compare can be performed
                //Exclude the current Earnings row on purpose because it is being edited
                var lstEarnings = _repoEarnings.SelectAll(earnings.StockId, earnings.EarningsId);

                //Reject duplicate entries
                CheckForDuplicates(lstEarnings, earnings, stock!.Symbol);

                //Order cannot be enforced because it depends on the user to input the dates and order correctly.
                _repoEarnings.Update(earnings);
            }
        }

        private static void CheckForDuplicates(IList<EarningsEntity> existingEarnings, EarningsEntity earnings, string symbol)
        {
            var dupDate = existingEarnings.Any(x => x.Date == earnings.Date);
            var dupOrder = existingEarnings.Any(x => x.Order == earnings.Order);

            if (!dupDate && !dupOrder) return;

            var part = dupDate ? EarningsExistsAlreadyException.Part.Date : EarningsExistsAlreadyException.Part.Order;

            throw new EarningsExistsAlreadyException(symbol, earnings, part);
        }

        //Earnings only affects reporting, so if it is deleted, then it just needs to be re-added.
        //I don't see a need to use effective dating yet.
        public void Delete(int earningsId)
        {
            _repoEarnings.Using(x => x.Delete(earningsId));
        }

        public void Delete(string symbol)
        {
            Validations.IsSymbolValid(symbol);

            if (_repoStock.Using(x => x.Select(symbol)) is null) throw NotFound.Symbol(symbol);

            _repoEarnings.Using(x => x.Delete(symbol));
        }

        //Parking this here for now, but really this is a generic thing
        public static DateTime GetYearAgnosticDate(DateTime date)
            => new (1753, date.Month, date.Day);

        public EarningsV1CreateMultipleModel TranslateToModel(IList<AddEarningsResult> results)
        {
            var success = results
                .Where(x => x.IsSuccessful)
                .Select(x => _mapper.ToModel(x.Earnings))
                .ToList();
            var failure = results
                .Where(x => !x.IsSuccessful)
                .Select(x => _mapper.ToFailedCreateModel(x))
                .ToList();

            var m = new EarningsV1CreateMultipleModel(success, failure);

            return m;
        }
    }
}
