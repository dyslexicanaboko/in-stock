using Dapper;
using InStock.Lib.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class PositionRepository
		: BaseRepository, IPositionRepository
	{
		public PositionEntity Select(int positionId)
		{
			var sql = @"
			SELECT
				PositionId,
				UserId,
				StockId,
				DateOpened,
				DateClosed,
				Price,
				Quantity,
				CreateOnUtc
			FROM dbo.Position
			WHERE PositionId = @PositionId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<PositionEntity>(sql, new { PositionId = positionId }).ToList();

				if (!lst.Any()) return null;

				var entity = lst.Single();

				return entity;
			}
		}

		public IEnumerable<PositionEntity> SelectAll()
		{
			var sql = @"
			SELECT
				PositionId,
				UserId,
				StockId,
				DateOpened,
				DateClosed,
				Price,
				Quantity,
				CreateOnUtc
			FROM dbo.Position";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<PositionEntity>(sql).ToList();

				return lst;
			}
		}

		
		public int Insert(PositionEntity entity)
		{
			var sql = @"INSERT INTO dbo.Position (
				UserId,
				StockId,
				DateOpened,
				DateClosed,
				Price,
				Quantity,
				CreateOnUtc
			) VALUES (
				@UserId,
				@StockId,
				@DateOpened,
				@DateClosed,
				@Price,
				@Quantity,
				@CreateOnUtc);

			SELECT SCOPE_IDENTITY() AS PK;";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
				p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
				p.Add(name: "@DateOpened", dbType: DbType.Date, value: entity.DateOpened);
				p.Add(name: "@DateClosed", dbType: DbType.Date, value: entity.DateClosed);
				p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
				p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

				return connection.ExecuteScalar<int>(sql, entity);
			}
		}

		public void Update(PositionEntity entity)
		{
			var sql = @"UPDATE dbo.Position SET 
				UserId = @UserId,
				StockId = @StockId,
				DateOpened = @DateOpened,
				DateClosed = @DateClosed,
				Price = @Price,
				Quantity = @Quantity,
				CreateOnUtc = @CreateOnUtc
			WHERE PositionId = @PositionId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@PositionId", dbType: DbType.Int32, value: entity.PositionId);
				p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
				p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
				p.Add(name: "@DateOpened", dbType: DbType.Date, value: entity.DateOpened);
				p.Add(name: "@DateClosed", dbType: DbType.Date, value: entity.DateClosed);
				p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
				p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

				connection.Execute(sql, p);
			}
		}
	}
}
