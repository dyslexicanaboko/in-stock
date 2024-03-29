using Dapper;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class StockRepository
		: BaseRepository, IStockRepository
	{
		public StockRepository()
		{
			
		}

		public StockRepository(IAppConfiguration configuration)
			: base(configuration)
		{
			
		}

		public StockEntity? Select(int stockId)
		{
			var sql = @"
			SELECT
				StockId,
				Symbol,
				Name,
				CreateOnUtc,
				Notes
			FROM dbo.Stock
			WHERE StockId = @stockId";

			var lst = GetConnection().Query<StockEntity>(sql, new { StockId = stockId }, Transaction).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public StockEntity? Select(string symbol)
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

			var lst = GetConnection().Query<StockEntity>(sql, new { symbol }, Transaction).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public StockEntity? SelectByEarningsId(int earningsId)
		{
			var sql = @"
			SELECT
				s.StockId,
				s.Symbol,
				s.Name,
				s.CreateOnUtc,
				s.Notes
			FROM dbo.Stock s
				INNER JOIN dbo.Earnings e
					ON s.StockId = e.StockId
			WHERE EarningsId = @earningsId";

			var lst = GetConnection().Query<StockEntity>(sql, new { earningsId }, Transaction).ToList();

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

			var lst = GetConnection().Query<StockEntity>(sql, transaction: Transaction).ToList();

			return lst;
		}

		public IEnumerable<StockEntity> Select(IList<int> stockIds)
		{
			var tvp = GetTvpIntegerList(stockIds);

			var sql = @"
			SELECT
				s.StockId,
				s.Symbol,
				s.Name,
				s.CreateOnUtc,
				s.Notes
			FROM dbo.Stock s
				INNER JOIN @tvp t
					ON s.StockId = t.IntValue";

			var lst = GetConnection().Query<StockEntity>(
				sql, 
				new { tvp }, 
				transaction: Transaction).ToList();

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

			return GetConnection().ExecuteScalar<int>(sql, entity, Transaction);
		}

		public void Update(StockEntity entity)
		{
			var sql = @"UPDATE dbo.Stock SET 
				Symbol = @Symbol,
				Name = @Name,
				Notes = @Notes,
				UpdatedOnUtc = SYSUTCDATETIME()
			WHERE StockId = @StockId";

			var p = new DynamicParameters();
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Symbol", dbType: DbType.AnsiString, value: entity.Symbol, size: 10);
			p.Add(name: "@Name", dbType: DbType.String, value: entity.Name, size: 255);
			p.Add(name: "@Notes", dbType: DbType.String, value: entity.Notes, size: 4000);

			GetConnection().Execute(sql, p, Transaction);
		}

		public void UpdateNotes(int stockId, string? notes)
		{
			var sql = @"UPDATE dbo.Stock SET 
				Notes = @notes,
				UpdatedOnUtc = SYSUTCDATETIME()
			WHERE StockId = @stockId";

			var p = new DynamicParameters();
			p.Add(name: "@stockId", dbType: DbType.Int32, value: stockId);
			p.Add(name: "@notes", dbType: DbType.String, value: notes, size: 4000);

			GetConnection().Execute(sql, p, Transaction);
		}
	}
}
