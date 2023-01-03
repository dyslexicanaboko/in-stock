namespace InStock.Lib.DataAccess
{
    public interface ITransactionManager
        : IDisposable
    {
        void Begin();
        void Commit();
        T GetRepository<T>() where T : BaseRepository, new();
    }
}