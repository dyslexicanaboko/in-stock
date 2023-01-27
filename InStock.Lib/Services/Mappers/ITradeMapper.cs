using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers;

public interface ITradeMapper
{
    TradeEntity? ToEntity(TradeModel? model);
    TradeEntity? ToEntity(ITrade? target);
    TradeEntity? ToEntity(TradeV1CreateModel? model);
    TradeModel? ToModel(TradeEntity? entity);
    TradeModel? ToModel(ITrade? target);
    IList<TradeModel> ToModel(IList<TradeEntity>? target);
}