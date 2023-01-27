namespace InStock.Lib.DataAccess
{
    public interface ITransactionManager
        : IDisposable
    {
        void Begin();

        void Commit();
        
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}