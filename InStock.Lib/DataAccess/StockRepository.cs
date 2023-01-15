using Dapper;
using InStock.Lib.Entities;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class StockRepository
		: BaseRepository, IStockRepository
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

			var lst = GetConnection().Query<StockEntity>(sql, new { StockId = stockId }, _transaction).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public StockEntity Select(string symbol)
		{
			var sql = @"
			SELECT
				StockId,
				Symbol,
				Name,
				CreateOnUtc,
				Notes
			FROM dbo.Stock
			WHERE Symbol = @symbol";

			var lst = GetConnection().Query<StockEntity>(sql, new { symbol }, _transaction).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
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

			var lst = GetConnection().Query<StockEntity>(sql, transaction: _transaction).ToList();

			return lst;
		}

		
		public int Insert(StockEntity entity)
		{
			var sql = @"INSERT INTO dbo.Stock (
				Symbol,
				Name,
				Notes
			) VALUES (
				@Symbol,
				@Name,
				@Notes);

			SELECT SCOPE_IDENTITY() AS PK;";

			var p = new DynamicParameters();
			p.Add(name: "@Symbol", dbType: DbType.AnsiString, value: entity.Symbol, size: 10);
			p.Add(name: "@Name", dbType: DbType.String, value: entity.Name, size: 255);
			p.Add(name: "@Notes", dbType: DbType.String, value: entity.Notes, size: 4000);

			return GetConnection().ExecuteScalar<int>(sql, entity, _transaction);
		}

		public void Update(StockEntity entity)
		{
			var sql = @"UPDATE dbo.Stock SET 
				Symbol = @Symbol,
				Name = @Name,
				Notes = @Notes
			WHERE StockId = @StockId";

			var p = new DynamicParameters();
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Symbol", dbType: DbType.AnsiString, value: entity.Symbol, size: 10);
			p.Add(name: "@Name", dbType: DbType.String, value: entity.Name, size: 255);
			p.Add(name: "@Notes", dbType: DbType.String, value: entity.Notes, size: 4000);

			GetConnection().Execute(sql, p, _transaction);
		}
	}
}
