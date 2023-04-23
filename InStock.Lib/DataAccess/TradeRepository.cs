using Dapper;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class TradeRepository
		: BaseRepository, ITradeRepository
	{
		public TradeRepository()
		{
			
		}

		public TradeRepository(IAppConfiguration configuration)
			: base(configuration)
		{

		}

		public TradeEntity? Select(int tradeId)
		{
			var sql = @"
			SELECT
				TradeId,
				UserId,
				StockId,
				TradeTypeId,
				Price,
				Quantity,
				ExecutionDate,
				CreateOnUtc,
				Confirmation
			FROM dbo.Trade
			WHERE TradeId = @TradeId";

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<TradeEntity>(sql, new { TradeId = tradeId }).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public IEnumerable<TradeEntity> Select(int userId, string symbol)
		{
			var sql = @"
			SELECT
				t.TradeId,
				t.UserId,
				t.StockId,
				t.TradeTypeId,
				t.Price,
				t.Quantity,
				t.ExecutionDate,
				t.CreateOnUtc,
				t.Confirmation
			FROM dbo.Stock s
				INNER JOIN dbo.Trade t
					ON s.StockId = t.StockId
						AND t.UserId = @userId
			WHERE s.Symbol = @symbol";

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<TradeEntity>(sql, new { userId, symbol }).ToList();

			return lst;
		}

		public IEnumerable<TradeEntity> SelectAll(int stockId, int? exceptTradeId = null)
		{
			var sql = @"
			SELECT
				TradeId,
				UserId,
				StockId,
				TradeTypeId,
				Price,
				Quantity,
				ExecutionDate,
				CreateOnUtc,
				Confirmation
			FROM dbo.Trade e
			WHERE e.StockId = @stockId";

			var parameters = new DynamicParameters();
			parameters.AddDynamicParams(new { stockId });


			if (exceptTradeId.HasValue)
			{
				sql += " AND e.TradeId <> @exceptTradeId";

				parameters.AddDynamicParams(new { exceptTradeId });
			}

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<TradeEntity>(sql, parameters).ToList();

			return lst;
		}

		public IEnumerable<TradeEntity> SelectAll()
		{
			var sql = @"
			SELECT
				TradeId,
				UserId,
				StockId,
				TradeTypeId,
				Price,
				Quantity,
				ExecutionDate,
				CreateOnUtc,
				Confirmation
			FROM dbo.Trade";

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<TradeEntity>(sql).ToList();

			return lst;
		}
		
		public int Insert(TradeEntity entity)
		{
			var sql = @"INSERT INTO dbo.Trade (
				UserId,
				StockId,
				TradeTypeId,
				Price,
				Quantity,
				ExecutionDate,
				Confirmation
			) VALUES (
				@UserId,
				@StockId,
				@TradeTypeId,
				@Price,
				@Quantity,
				@ExecutionDate,
				@Confirmation);

			SELECT SCOPE_IDENTITY() AS PK;";

			using var connection = new SqlConnection(ConnectionString);
			
			var p = new DynamicParameters();
			p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@TradeTypeId", dbType: DbType.Int32, value: entity.TradeTypeId);
			p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
			p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);
			p.Add(name: "@ExecutionDate", dbType: DbType.DateTime2, value: entity.ExecutionDate, scale: 0);
			p.Add(name: "@Confirmation", dbType: DbType.AnsiString, value: entity.Confirmation, size: 50);

			return connection.ExecuteScalar<int>(sql, entity);
		}

		public void Update(TradeEntity entity)
		{
			var sql = @"UPDATE dbo.Trade SET 
				TradeTypeId = @TradeTypeId,
				Price = @Price,
				Quantity = @Quantity,
				ExecutionDate = @ExecutionDate,
				Confirmation = @Confirmation,
				UpdatedOnUtc = SYSUTCDATETIME()
			WHERE TradeId = @TradeId";

			using var connection = new SqlConnection(ConnectionString);
			
			var p = new DynamicParameters();
			p.Add(name: "@TradeId", dbType: DbType.Int32, value: entity.TradeId);
			p.Add(name: "@TradeTypeId", dbType: DbType.Int32, value: entity.TradeTypeId);
			p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
			p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);
			p.Add(name: "@ExecutionDate", dbType: DbType.DateTime2, value: entity.ExecutionDate, scale: 0);
			p.Add(name: "@Confirmation", dbType: DbType.AnsiString, value: entity.Confirmation, size: 50);

			connection.Execute(sql, p);
		}

		public void Delete(int tradeId)
		{
			var sql = @"DELETE FROM dbo.Trade WHERE TradeId = @tradeId";

			using var connection = new SqlConnection(ConnectionString);

			connection.Execute(sql, new { tradeId });
		}

		public void Delete(int userId, string symbol)
		{
			var sql = @"DELETE t 
				FROM dbo.Stock s
				INNER JOIN dbo.Trade t
					ON s.StockId = t.StockId
						AND t.UserId = @userId
			WHERE s.Symbol = @symbol";

			using var connection = new SqlConnection(ConnectionString);

			connection.Execute(sql, new { userId, symbol });
		}
	}
}
