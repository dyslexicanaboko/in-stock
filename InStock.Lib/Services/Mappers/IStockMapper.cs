using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public interface IStockMapper
    {
        StockV1CreatedModel ToCreatedModel(IStock target);
        StockEntity ToEntity(IStock target);
        StockEntity ToEntity(StockModel model);
        StockModel ToModel(IStock target);
        StockModel ToModel(StockEntity entity);
    }
}