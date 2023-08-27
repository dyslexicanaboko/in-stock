using Dapper;
using InStock.Lib.Entities.Composites;
using InStock.Lib.Services;
using Microsoft.Data.SqlClient;

namespace InStock.Lib.DataAccess
{
	public class PortfolioRepository
		: BaseRepository, IPortfolioRepository
	{
		public PortfolioRepository(IAppConfiguration configuration)
			: base(configuration)
		{
			
		}

		public IEnumerable<PortfolioComposite> Select(int userId)
		{
			const string sql = @"
			SELECT
					s.StockId
				 ,s.Symbol
				 ,MIN(p.DateOpened) AS AcquiredOn
				 ,SUM(p.Quantity) AS Shares
				 ,SUM(p.Quantity * p.Price) AS CostBasis
				 ,MIN(p.Price) AS LowestHeld
				 ,MAX(p.Price) AS HighestHeld
				 ,SUM(IIF(DATEDIFF(DAY, p.DateOpened, SYSUTCDATETIME()) < 365, 1, 0)) AS Short
				 ,SUM(IIF(DATEDIFF(DAY, p.DateOpened, SYSUTCDATETIME()) >= 365, 1, 0)) AS Long
			FROM dbo.Position p
				INNER JOIN dbo.Stock s
					ON s.StockId = p.StockId
			WHERE p.UserId = @userId
			GROUP BY s.StockId, s.Symbol";

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<PortfolioComposite>(sql, new { userId }).ToList();

			return lst;
		}
	}
}
