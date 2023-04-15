using CommunityToolkit.Diagnostics;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;

namespace InStock.Lib.Services
{
    public class EarningsService : IEarningsService
    {
        public const int MaxEntries = 4;
        private readonly IEarningsRepository _repoEarnings;
        private readonly IStockRepository _repoStock;

        public EarningsService(
            IEarningsRepository repoEarnings,
            IStockRepository repoStock)
        {
            _repoEarnings = repoEarnings;
         
            _repoStock = repoStock;
        }

        public EarningsEntity? GetEarnings(int id)
        {
            var dbEntity = _repoEarnings.Using(x => x.Select(id));

            return dbEntity;
        }

        public IList<EarningsEntity>? GetEarnings(string symbol)
        {
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));

            if (_repoStock.Using(x => x.Select(symbol)) is null) throw new SymbolNotFoundException(symbol);

            var dbEntity = _repoEarnings.Using(x => x.Select(symbol));

            return dbEntity;
        }

        public EarningsEntity Add(EarningsEntity? earnings)
        {
            Guard.IsNotNull(earnings);

            //Years are meaningless here, so they will be minified on purpose.
            earnings.Date = GetYearAgnosticDate(earnings.Date);

            var stock = _repoStock.Using(x => x.Select(earnings.StockId));

            if (stock is null) throw new StockNotFoundException(earnings.StockId);

            using (_repoEarnings)
            {
                //To enforce uniqueness, all earnings must be returned so a compare can be performed
                var lstEarnings = _repoEarnings.SelectAll(earnings.StockId);

                //Enforce only four entries
                if (lstEarnings.Count == MaxEntries)
                    throw new MaxEntriesException(stock.Symbol, "earnings", MaxEntries);

                //Reject duplicate entries
                CheckForDuplicates(lstEarnings, earnings, stock.Symbol);

                //Order cannot be enforced because it depends on the user to input the dates and order correctly.
                earnings.EarningsId = _repoEarnings.Insert(earnings);

                return earnings;
            }
        }

        public void Edit(EarningsEntity? earnings)
        {
            Guard.IsNotNull(earnings);

            //Years are meaningless here, so they will be minified on purpose.
            earnings.Date = GetYearAgnosticDate(earnings.Date);

            var stock = _repoStock.Using(x => x.Select(earnings.StockId));

            if (stock is null) throw new StockNotFoundException(earnings.StockId);

            using (_repoEarnings)
            {
                //To enforce uniqueness, all earnings must be returned so a compare can be performed
                //Exclude the current Earnings row on purpose because it is being edited
                var lstEarnings = _repoEarnings.SelectAll(earnings.StockId, earnings.EarningsId);

                //Enforce the four entries here is not necessary since we are only doing a scalar edit

                //Reject duplicate entries
                CheckForDuplicates(lstEarnings, earnings, stock.Symbol);

                //Order cannot be enforced because it depends on the user to input the dates and order correctly.
                _repoEarnings.Update(earnings);
            }
        }

        private void CheckForDuplicates(IList<EarningsEntity> existingEarnings, EarningsEntity earnings, string symbol)
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
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));

            if (_repoStock.Using(x => x.Select(symbol)) is null) throw new SymbolNotFoundException(symbol);

            _repoEarnings.Using(x => x.Delete(symbol));
        }

        //Parking this here for now, but really this is a generic thing
        public static DateTime GetYearAgnosticDate(DateTime date)
            => new (1753, date.Month, date.Day);
    }
}
