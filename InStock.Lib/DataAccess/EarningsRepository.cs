using Dapper;
using InStock.Lib.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class EarningsRepository
		: BaseRepository, IRepository<EarningsEntity>
	{
		public EarningsEntity Select(int earningsId)
		{
			var sql = @"
			SELECT
				EarningsId,
				StockId,
				Date,
				Order,
				CreateOnUtc
			FROM dbo.Earnings
			WHERE EarningsId = @EarningsId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<EarningsEntity>(sql, new { EarningsId = earningsId }).ToList();

				if (!lst.Any()) return null;

				var entity = lst.Single();

				return entity;
			}
		}

		public IEnumerable<EarningsEntity> SelectAll()
		{
			var sql = @"
			SELECT
				EarningsId,
				StockId,
				Date,
				Order,
				CreateOnUtc
			FROM dbo.Earnings";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<EarningsEntity>(sql).ToList();

				return lst;
			}
		}

		//Preference on whether or not insert method returns a value is up to the user and the object being inserted
		public int Insert(EarningsEntity entity)
		{
			var sql = @"INSERT INTO dbo.Earnings (
				StockId,
				Date,
				Order,
				CreateOnUtc
			) VALUES (
				@StockId,
				@Date,
				@Order,
				@CreateOnUtc);

			SELECT SCOPE_IDENTITY() AS PK;";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
				p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
				p.Add(name: "@Order", dbType: DbType.Int32, value: entity.Order);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

				return connection.ExecuteScalar<int>(sql, entity);
			}
		}

		public void Update(EarningsEntity entity)
		{
			var sql = @"UPDATE dbo.Earnings SET 
				StockId = @StockId,
				Date = @Date,
				Order = @Order,
				CreateOnUtc = @CreateOnUtc
			WHERE EarningsId = @EarningsId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@EarningsId", dbType: DbType.Int32, value: entity.EarningsId);
				p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
				p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
				p.Add(name: "@Order", dbType: DbType.Int32, value: entity.Order);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

				connection.Execute(sql, p);
			}
		}
	}
}
