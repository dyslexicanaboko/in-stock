using Dapper;
using InStock.Lib.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class StockRepository
		: BaseRepository, IRepository<StockEntity>
	{
		public StockEntity Select(int stockId)
		{
			var sql = @"
			SELECT
				StockId,
				Symbol,
				Name,
				CreateOnUtc,
				Notes
			FROM dbo.Stock
			WHERE StockId = @StockId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<StockEntity>(sql, new { StockId = stockId }).ToList();

				if (!lst.Any()) return null;

				var entity = lst.Single();

				return entity;
			}
		}

		public IEnumerable<StockEntity> SelectAll()
		{
			var sql = @"
			SELECT
				StockId,
				Symbol,
				Name,
				CreateOnUtc,
				Notes
			FROM dbo.Stock";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<StockEntity>(sql).ToList();

				return lst;
			}
		}

		//Preference on whether or not insert method returns a value is up to the user and the object being inserted
		public int Insert(StockEntity entity)
		{
			var sql = @"INSERT INTO dbo.Stock (
				Symbol,
				Name,
				CreateOnUtc,
				Notes
			) VALUES (
				@Symbol,
				@Name,
				@CreateOnUtc,
				@Notes);

			SELECT SCOPE_IDENTITY() AS PK;";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@Symbol", dbType: DbType.AnsiString, value: entity.Symbol, size: 10);
				p.Add(name: "@Name", dbType: DbType.String, value: entity.Name, size: 255);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);
				p.Add(name: "@Notes", dbType: DbType.String, value: entity.Notes, size: 4000);

				return connection.ExecuteScalar<int>(sql, entity);
			}
		}

		public void Update(StockEntity entity)
		{
			var sql = @"UPDATE dbo.Stock SET 
				Symbol = @Symbol,
				Name = @Name,
				CreateOnUtc = @CreateOnUtc,
				Notes = @Notes
			WHERE StockId = @StockId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
				p.Add(name: "@Symbol", dbType: DbType.AnsiString, value: entity.Symbol, size: 10);
				p.Add(name: "@Name", dbType: DbType.String, value: entity.Name, size: 255);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);
				p.Add(name: "@Notes", dbType: DbType.String, value: entity.Notes, size: 4000);

				connection.Execute(sql, p);
			}
		}
	}
}
