using Dapper;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class EarningsRepository
		: BaseRepository, IEarningsRepository
	{
		public EarningsRepository()
		{
			
		}

		public EarningsRepository(IAppConfiguration configuration)
			: base(configuration)
		{

		}

		public EarningsEntity? Select(int earningsId)
		{
			var sql = @"
			SELECT
				EarningsId,
				StockId,
				[Date],
				[Order],
				CreateOnUtc
			FROM dbo.Earnings
			WHERE EarningsId = @EarningsId";

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<EarningsEntity>(sql, new { EarningsId = earningsId }).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public IList<EarningsEntity> Select(string symbol)
		{
			var sql = @"
			SELECT
				e.EarningsId,
				e.StockId,
				e.[Date],
				e.[Order],
				e.CreateOnUtc
			FROM dbo.Stock s
				INNER JOIN dbo.Earnings e
					ON s.StockId = e.StockId
			WHERE s.Symbol = @symbol";

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<EarningsEntity>(sql, new { symbol }).ToList();

			return lst;
		}

		public IList<EarningsEntity> SelectAll(int stockId, int? exceptEarningsId = null)
		{
			var sql = @"
			SELECT
				e.EarningsId,
				e.StockId,
				e.[Date],
				e.[Order],
				e.CreateOnUtc
			FROM dbo.Earnings e
			WHERE e.StockId = @stockId";

			var parameters = new DynamicParameters();
			parameters.AddDynamicParams(new { stockId });


			if (exceptEarningsId.HasValue)
			{
				sql += " AND e.EarningsId <> @exceptEarningsId";

				parameters.AddDynamicParams(new { exceptEarningsId });
			}

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<EarningsEntity>(sql, parameters).ToList();

			return lst;
		}

		public IEnumerable<EarningsEntity> SelectAll()
		{
			var sql = @"
			SELECT
				EarningsId,
				StockId,
				[Date],
				[Order],
				CreateOnUtc
			FROM dbo.Earnings";

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<EarningsEntity>(sql).ToList();

			return lst;
		}

		
		public int Insert(EarningsEntity entity)
		{
			var sql = @"INSERT INTO dbo.Earnings (
				StockId,
				[Date],
				[Order]
			) VALUES (
				@StockId,
				@Date,
				@Order);

			SELECT SCOPE_IDENTITY() AS PK;";

			using var connection = new SqlConnection(ConnectionString);
			
			var p = new DynamicParameters();
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
			p.Add(name: "@Order", dbType: DbType.Int32, value: entity.Order);

			return connection.ExecuteScalar<int>(sql, entity);
		}

		public void Update(EarningsEntity entity)
		{
			var sql = @"UPDATE dbo.Earnings SET 
				StockId = @StockId,
				[Date] = @Date,
				[Order] = @Order
			WHERE EarningsId = @EarningsId";

			using var connection = new SqlConnection(ConnectionString);
			
			var p = new DynamicParameters();
			p.Add(name: "@EarningsId", dbType: DbType.Int32, value: entity.EarningsId);
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
			p.Add(name: "@Order", dbType: DbType.Int32, value: entity.Order);

			connection.Execute(sql, p);
		}

		public void Delete(int earningsId)
		{
			var sql = @"DELETE FROM dbo.Earnings WHERE EarningsId = @earningsId";

			using var connection = new SqlConnection(ConnectionString);
			
			connection.Execute(sql, new { earningsId });
		}

		public void Delete(string symbol)
		{
			var sql = @"DELETE e 
				FROM dbo.Stock s
				INNER JOIN dbo.Earnings e
					ON s.StockId = e.StockId
			WHERE s.Symbol = @symbol";

			using var connection = new SqlConnection(ConnectionString);
			
			connection.Execute(sql, new { symbol });
		}
	}
}
