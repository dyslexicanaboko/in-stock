namespace InStock.Lib.DataAccess
{
    public interface IRepository<T>
        : IDisposable
        where T : class, new()
    {
        T Select(int earningsId);

        IEnumerable<T> SelectAll();

        int Insert(T entity);

        void Update(T entity);
    }
}
