using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStock.Lib.DataAccess
{
    public class TransactionManager
        : BaseRepository, IDisposable
    {
        private readonly Dictionary<string, object> _repositories;

        public TransactionManager()
            : base()
        {
            _repositories = new Dictionary<string, object>();
        }

        public void Begin()
        {
            _connection.Open();
            
            _transaction = _connection.BeginTransaction();
        }

        public void Commit() => _transaction?.Commit();

        public T GetRepository<T>()
            where T : BaseRepository, new()
        {
            var t = typeof(T);

            if (_repositories.ContainsKey(t.Name)) return (T)_repositories[t.Name];

            var repo = new T(_transaction);

            _repositories.Add(t.Name, t);

            return 
        }
    }
}
