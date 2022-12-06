using InStock.Lib.Entities;
using InStock.Lib.Models;

namespace InStock.Lib.Services.Mappers
{
    //https://stackoverflow.com/questions/1096568/how-can-i-use-interface-as-a-c-sharp-generic-type-constraint
    public interface IMapper<TSharedInterface, TEntity, TModel>
        where TSharedInterface : class //Should be restricted to interface, but this syntax does not exist
        where TEntity : class, new()
        where TModel : class, new()
    {
        TEntity ToEntity(TModel model);

        TModel ToModel(TEntity entity);

        TEntity ToEntity(TSharedInterface target);

        TModel ToModel(TSharedInterface target);
    }
}
