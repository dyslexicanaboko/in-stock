using Dapper;
using InStock.Lib.Entities;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class QuoteRepository
		: BaseRepository, IQuoteRepository
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

			
			var lst = GetConnection().Query<QuoteEntity>(sql, new { QuoteId = quoteId }, _transaction).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
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

			var lst = GetConnection().Query<QuoteEntity>(sql, transaction: _transaction).ToList();

			return lst;
		}

		
		public int Insert(QuoteEntity entity)
		{
			var sql = @"INSERT INTO dbo.Quote (
				StockId,
				Date,
				Price,
				Volume,
			) VALUES (
				@StockId,
				@Date,
				@Price,
				@Volume);

			SELECT SCOPE_IDENTITY() AS PK;";
			
			var p = new DynamicParameters();
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
			p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
			p.Add(name: "@Volume", dbType: DbType.Decimal, value: entity.Volume, precision: 10, scale: 2);

			return GetConnection().ExecuteScalar<int>(sql, entity, _transaction);
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

			var p = new DynamicParameters();
			p.Add(name: "@QuoteId", dbType: DbType.Int32, value: entity.QuoteId);
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
			p.Add(name: "@Price", dbType: DbType.Decimal, value: entity.Price, precision: 10, scale: 2);
			p.Add(name: "@Volume", dbType: DbType.Decimal, value: entity.Volume, precision: 10, scale: 2);
			p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

			GetConnection().Execute(sql, p, _transaction);
		}
	}
}
