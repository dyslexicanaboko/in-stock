using InStock.Lib.Entities.Results;

namespace InStock.Lib.Services.Factory;

public interface IGainFactory
{
  GainResult Create(decimal totalValue, decimal costBasis);
}
