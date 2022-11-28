//https://www.nuget.org/packages/Dapper/
using Dapper;
using InStock.Lib.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
    public class QuoteRepository
        : BaseRepository
    {
        public QuoteEntity Select(int quoteId)
        {
            var sql = @"
			SELECT
				QuoteId,
				StockId,
				Date,
				Price,
				Volume,
				CreateOnUtc
			FROM dbo.Quote
			WHERE QuoteId = @QuoteId";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var lst = connection.Query<QuoteEntity>(sql, new { QuoteId = quoteId }).ToList();

                if (!lst.Any()) return null;

                var entity = lst.Single();

                return entity;
            }
        }

        public IEnumerable<QuoteEntity> SelectAll()
        {
            var sql = @"
			SELECT
				QuoteId,
				StockId,
				Date,
				Price,
				Volume,
				CreateOnUtc
			FROM dbo.Quote";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var lst = connection.Query<QuoteEntity>(sql).ToList();

                return lst;
            }
        }

        //Preference on whether or not insert method returns a value is up to the user and the object being inserted
        public int Insert(QuoteEntity entity)
        {
            var sql = @"INSERT INTO dbo.Quote (
				StockId,
				Date,
				Price,
				Volume,
				CreateOnUtc
			) VALUES (
				@StockId,
				@Date,
				@Price,
				@Volume,
				@CreateOnUtc);

			SELECT SCOPE_IDENTITY() AS PK;";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
                p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
                p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
                p.Add(name: "@Volume", dbType: DbType.Decimal, value: entity.Volume, precision: 10, scale: 2);
                p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

                return connection.ExecuteScalar<int>(sql, entity);
            }
        }

        public void Update(QuoteEntity entity)
        {
            var sql = @"UPDATE dbo.Quote SET 
				StockId = @StockId,
				Date = @Date,
				Price = @Price,
				Volume = @Volume,
				CreateOnUtc = @CreateOnUtc
			WHERE QuoteId = @QuoteId";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add(name: "@QuoteId", dbType: DbType.Int32, value: entity.QuoteId);
                p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
                p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
                p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
                p.Add(name: "@Volume", dbType: DbType.Decimal, value: entity.Volume, precision: 10, scale: 2);
                p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

                connection.Execute(sql, p);
            }
        }
    }
}
