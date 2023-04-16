using InStock.Lib.Entities;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services
{
    public interface IEarningsService
    {
        IList<AddEarningsResult> Add(EarningsEntity earnings);

        IList<AddEarningsResult> Add(IList<EarningsEntity>? multipleEarnings);

        void Edit(EarningsEntity? earnings);

        void Delete(int earningsId);
        
        void Delete(string symbol);
        
        EarningsEntity? GetEarnings(int id);

        EarningsV1CreateMultipleModel TranslateToModel(IList<AddEarningsResult> results);

        IList<EarningsEntity>? GetEarnings(string symbol);
    }
}