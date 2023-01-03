namespace InStock.Lib.DataAccess
{
    public static class RepositoryExtensions
    {
        public static TReturn Using<TRepo, TReturn>(this TRepo repo, Func<TRepo, TReturn> method)
            where TRepo : IRepository<TReturn>
            where TReturn: class, new()
        {
            using (repo)
            {
                return method(repo);
            }
        }
    }
}
