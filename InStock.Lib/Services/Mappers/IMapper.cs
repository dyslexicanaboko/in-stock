//TODO: Move to graveyard, no longer in use
namespace InStock.Lib.Services.Mappers
{
    //https://stackoverflow.com/questions/1096568/how-can-i-use-interface-as-a-c-sharp-generic-type-constraint
    public interface IMapper<TSharedInterface, TEntity, TModel>
        where TSharedInterface : class //Should be restricted to interface, but this syntax does not exist
        where TEntity : class, new()
        where TModel : class
    {
        public TEntity? ToEntity(TModel? model);

        public TModel? ToModel(TEntity? entity);

        public TEntity? ToEntity(TSharedInterface? target);

        public TModel? ToModel(TSharedInterface? target);
    }
}
