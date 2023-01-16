using InStock.Lib.DataAccess;
using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public class EarningsService : IEarningsService
    {
        private readonly IEarningsRepository _repoEarnings;

        public EarningsService(
            IEarningsRepository repoEarnings)
        {
            _repoEarnings = repoEarnings;
        }

        public EarningsEntity? GetEarnings(int id)
        {
            var dbEntity = _repoEarnings.Using(x => x.Select(id));

            return dbEntity;
        }

        //TODO: Need to check for existince of the symbol?
        public IList<EarningsEntity> GetEarnings(string symbol)
        {
            var dbEntity = _repoEarnings.Using(x => x.Select(symbol));

            return dbEntity;
        }

        //TODO: Enforce only four entries?
        //TODO: Prevent duplicate entries of order or dates
        //TODO: Enforce a proper order or let it be Gigo?
        //TODO: Enforce FK of StockId existence
        public EarningsEntity Add(EarningsEntity earnings)
        {
            earnings.EarningsId = _repoEarnings.Using(x => x.Insert(earnings));

            return earnings;
        }

        //Earnings only affects reporting, so if it is deleted, then it just needs to be re-added.
        //I don't see a need to use effective dating yet.
        public void Delete(int earningsId)
        {
            _repoEarnings.Using(x => x.Delete(earningsId));
        }

        //TODO: Need to check for existince of the symbol?
        public void Delete(string symbol)
        {
            _repoEarnings.Using(x => x.Delete(symbol));
        }
    }
}
