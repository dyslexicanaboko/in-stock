using Dapper;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class QuoteRepository
		: BaseRepository, IQuoteRepository
	{
		public QuoteRepository()
		{
			
		}

		public QuoteRepository(IAppConfiguration configuration)
			: base(configuration)
		{

		}

		public QuoteEntity? Select(int quoteId)
		{
			var sql = @"
			SELECT
				QuoteId,
				StockId,
				Date,
				Price,
				Volume,
				CreatedOnUtc
			FROM dbo.Quote
			WHERE QuoteId = @QuoteId";

			
			var lst = GetConnection().Query<QuoteEntity>(sql, new { QuoteId = quoteId }, Transaction).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public QuoteEntity? Select(string symbol)
		{
			var sql = @"
			SELECT TOP 1
				q.QuoteId,
				q.StockId,
				q.Date,
				q.Price,
				q.Volume,
				q.CreatedOnUtc
			FROM dbo.Stock s
				INNER JOIN dbo.Quote q
					ON s.StockId = q.StockId
			WHERE s.Symbol = @symbol
			ORDER BY q.Date DESC, q.CreatedOnUtc DESC ";

			var lst = GetConnection().Query<QuoteEntity>(sql, new { symbol }, Transaction).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public QuoteEntity? SelectRecent(string symbol, DateTime relativeTimeUtc, int lastXMinutes)
		{
			var dtmStart = relativeTimeUtc.AddMinutes(-lastXMinutes);

			var sql = @"
			SELECT TOP 1
				q.QuoteId,
				q.StockId,
				q.Date,
				q.Price,
				q.Volume,
				q.CreatedOnUtc
			FROM dbo.Stock s
				INNER JOIN dbo.Quote q
					ON s.StockId = q.StockId
			WHERE s.Symbol = @symbol
				AND q.CreatedOnUtc >= @dtmStart AND @relativeTimeUtc <= q.CreatedOnUtc
			ORDER BY q.Date DESC, q.CreatedOnUtc DESC ";

			var lst = GetConnection().Query<QuoteEntity>(sql, new
				{
					symbol,
					dtmStart,
					relativeTimeUtc
				}
				, Transaction).ToList();

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
				CreatedOnUtc
			FROM dbo.Quote";

			var lst = GetConnection().Query<QuoteEntity>(sql, transaction: Transaction).ToList();

			return lst;
		}

		
		public int Insert(QuoteEntity entity)
		{
			var sql = @"INSERT INTO dbo.Quote (
				StockId,
				Date,
				Price,
				Volume
			) VALUES (
				@StockId,
				@Date,
				@Price,
				@Volume);

			SELECT SCOPE_IDENTITY() AS PK;";
			
			var p = new DynamicParameters();
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
			p.Add(name: "@Price", dbType: DbType.Double, value: entity.Price);
			p.Add(name: "@Volume", dbType: DbType.Int64, value: entity.Volume);

			return GetConnection().ExecuteScalar<int>(sql, entity, Transaction);
		}

		public void Update(QuoteEntity entity)
		{
			var sql = @"UPDATE dbo.Quote SET 
				StockId = @StockId,
				Date = @Date,
				Price = @Price,
				Volume = @Volume
			WHERE QuoteId = @QuoteId";

			var p = new DynamicParameters();
			p.Add(name: "@QuoteId", dbType: DbType.Int32, value: entity.QuoteId);
			p.Add(name: "@StockId", dbType: DbType.Int32, value: entity.StockId);
			p.Add(name: "@Date", dbType: DbType.Date, value: entity.Date);
			p.Add(name: "@Price", dbType: DbType.Double, value: entity.Price);
			p.Add(name: "@Volume", dbType: DbType.Int64, value: entity.Volume);

			GetConnection().Execute(sql, p, Transaction);
		}
	}
}
