using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers;

public interface ITradeMapper
{
    TradeEntity? ToEntity(TradeModel? model);
    
    TradeEntity? ToEntity(ITrade? target);
    
    TradeEntity? ToEntity(int userId, TradeV1CreateModel? model);
    
    TradeModel? ToModel(TradeEntity? entity);
    
    TradeModel? ToModel(ITrade? target);

    TradeV1CreateMultipleModel ToModel(IList<AddTradeResult> results);

    TradeV1FailedCreateModel? ToFailedCreateModel(AddTradeResult? result);
    
    IList<TradeModel> ToModel(IList<TradeEntity>? target);

    IList<TradeEntity> ToEntity(int userId, IList<TradeV1CreateModel>? target);
}