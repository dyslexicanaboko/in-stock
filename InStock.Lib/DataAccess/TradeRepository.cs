using Dapper;
using InStock.Lib.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class TradeRepository
		: BaseRepository, ITradeRepository
	{
		public TradeEntity Select(int tradeId)
		{
			var sql = @"
			SELECT
				TradeId,
				UserId,
				StockId,
				Type,
				Price,
				Quantity,
				StartDate,
				EndDate,
				CreateOnUtc,
				Confirmation
			FROM dbo.Trade
			WHERE TradeId = @TradeId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<TradeEntity>(sql, new { TradeId = tradeId }).ToList();

				if (!lst.Any()) return null;

				var entity = lst.Single();

				return entity;
			}
		}

		public IEnumerable<TradeEntity> SelectAll()
		{
			var sql = @"
			SELECT
				TradeId,
				UserId,
				StockId,
				Type,
				Price,
				Quantity,
				StartDate,
				EndDate,
				CreateOnUtc,
				Confirmation
			FROM dbo.Trade";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<TradeEntity>(sql).ToList();

				return lst;
			}
		}

		
		public int Insert(TradeEntity entity)
		{
			var sql = @"INSERT INTO dbo.Trade (
				UserId,
				StockId,
				Type,
				Price,
				Quantity,
				StartDate,
				EndDate,
				Confirmation
			) VALUES (
				@UserId,
				@StockId,
				@Type,
				@Price,
				@Quantity,
				@StartDate,
				@EndDate,
				@Confirmation);

			SELECT SCOPE_IDENTITY() AS PK;";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
				p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
				p.Add(name: "@Type", dbType: DbType.Boolean, value: entity.Type);
				p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
				p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);
				p.Add(name: "@StartDate", dbType: DbType.Date, value: entity.StartDate);
				p.Add(name: "@EndDate", dbType: DbType.Date, value: entity.EndDate);
				p.Add(name: "@Confirmation", dbType: DbType.AnsiString, value: entity.Confirmation, size: 50);

				return connection.ExecuteScalar<int>(sql, entity);
			}
		}

		public void Update(TradeEntity entity)
		{
			var sql = @"UPDATE dbo.Trade SET 
				UserId = @UserId,
				StockId = @StockId,
				Type = @Type,
				Price = @Price,
				Quantity = @Quantity,
				StartDate = @StartDate,
				EndDate = @EndDate,
				CreateOnUtc = @CreateOnUtc,
				Confirmation = @Confirmation
			WHERE TradeId = @TradeId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@TradeId", dbType: DbType.Int32, value: entity.TradeId);
				p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
				p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
				p.Add(name: "@Type", dbType: DbType.Boolean, value: entity.Type);
				p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
				p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);
				p.Add(name: "@StartDate", dbType: DbType.Date, value: entity.StartDate);
				p.Add(name: "@EndDate", dbType: DbType.Date, value: entity.EndDate);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);
				p.Add(name: "@Confirmation", dbType: DbType.AnsiString, value: entity.Confirmation, size: 50);

				connection.Execute(sql, p);
			}
		}
	}
}
