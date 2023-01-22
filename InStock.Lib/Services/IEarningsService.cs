using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public interface IEarningsService
    {
        EarningsEntity Add(EarningsEntity? earnings);
        
        void Delete(int earningsId);
        
        void Delete(string symbol);
        
        EarningsEntity? GetEarnings(int id);
        
        IList<EarningsEntity>? GetEarnings(string symbol);
    }
}