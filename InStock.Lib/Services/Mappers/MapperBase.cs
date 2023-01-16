namespace InStock.Lib.Services.Mappers
{
    public abstract class MapperBase
    {
        protected IList<TModel> ToList<TEntity, TModel>(IList<TEntity?> target, Func<TEntity?, TModel?> mapper)
            where TEntity : class, new()
            where TModel : class, new()
        {
            if (target == null || !target.Any()) return new List<TModel>();

            var lst = target.Where(x => x == null).Select(mapper).ToList();

            return lst!;
        }
    }
}
