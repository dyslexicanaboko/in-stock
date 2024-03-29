﻿using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface IEarningsRepository
        : IRepository<EarningsEntity>
    {
        IList<EarningsEntity> Select(string symbol);

        IList<EarningsEntity> SelectAll(int stockId, int? exceptEarningsId = null);

        void Delete(int earningsId);
        
        void Delete(string symbol);
    }
}
