using Dapper;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class PositionRepository
		: BaseRepository, IPositionRepository
	{
		public PositionRepository()
		{
			
		}

		public PositionRepository(IAppConfiguration configuration)
			: base(configuration)
		{

		}

		public PositionEntity? Select(int positionId)
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

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<PositionEntity>(sql, new { PositionId = positionId }).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public IEnumerable<PositionEntity> Select(int userId, string symbol)
		{
			var sql = @"
			SELECT
				p.PositionId,
				p.UserId,
				p.StockId,
				p.DateOpened,
				p.DateClosed,
				p.Price,
				p.Quantity,
				p.CreateOnUtc
			FROM dbo.Stock s
				INNER JOIN dbo.Position p
					ON s.StockId = p.StockId
						AND p.UserId = @userId
			WHERE s.Symbol = @symbol";

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<PositionEntity>(sql, new { userId, symbol }).ToList();

			return lst;
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

			using var connection = new SqlConnection(ConnectionString);
			
			var lst = connection.Query<PositionEntity>(sql).ToList();

			return lst;
		}

		
		public int Insert(PositionEntity entity)
		{
			var sql = @"INSERT INTO dbo.Position (
				UserId,
				StockId,
				DateOpened,
				DateClosed,
				Price,
				Quantity
			) VALUES (
				@UserId,
				@StockId,
				@DateOpened,
				@DateClosed,
				@Price,
				@Quantity);

			SELECT SCOPE_IDENTITY() AS PK;";

			using var connection = new SqlConnection(ConnectionString);
			
			var p = new DynamicParameters();
			p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@DateOpened", dbType: DbType.DateTime2, value: entity.DateOpened, scale: 0);
			p.Add(name: "@DateClosed", dbType: DbType.DateTime2, value: entity.DateClosed, scale: 0);
			p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
			p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);

			return connection.ExecuteScalar<int>(sql, entity);
		}

		public void Update(PositionEntity entity)
		{
			var sql = @"UPDATE dbo.Position SET 
				UserId = @UserId,
				StockId = @StockId,
				DateOpened = @DateOpened,
				DateClosed = @DateClosed,
				Price = @Price,
				Quantity = @Quantity
			WHERE PositionId = @PositionId";

			using var connection = new SqlConnection(ConnectionString);
			
			var p = new DynamicParameters();
			p.Add(name: "@PositionId", dbType: DbType.Int32, value: entity.PositionId);
			p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@DateOpened", dbType: DbType.DateTime2, value: entity.DateOpened, scale: 0);
			p.Add(name: "@DateClosed", dbType: DbType.DateTime2, value: entity.DateClosed, scale: 0);
			p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
			p.Add(name: "@Quantity", dbType: DbType.Decimal, value: entity.Quantity, precision: 10, scale: 2);

			connection.Execute(sql, p);
		}

		public void Delete(int positionId)
		{
			var sql = @"DELETE FROM dbo.Position WHERE PositionId = @positionId";

			using var connection = new SqlConnection(ConnectionString);
			
			connection.Execute(sql, new { positionId });
		}

		public void Delete(int userId, string symbol)
		{
			var sql = @"DELETE p 
				FROM dbo.Stock s
				INNER JOIN dbo.Position p
					ON s.StockId = p.StockId
						AND p.UserId = @userId
			WHERE s.Symbol = @symbol";

			using var connection = new SqlConnection(ConnectionString);
			
			connection.Execute(sql, new { userId, symbol });
		}
	}
}
