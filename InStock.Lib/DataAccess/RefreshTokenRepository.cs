using Dapper;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class RefreshTokenRepository
		: BaseRepository, IRefreshTokenRepository
	{
		public RefreshTokenRepository(IAppConfiguration configuration)
			: base(configuration)
		{

		}

		public IEnumerable<RefreshTokenEntity> SelectByUserId(int userId)
		{
			const string sql = @"
			SELECT
				RefreshTokenId
			 ,UserId
			 ,Token
			 ,CreatedOn
			 ,ExpiresOn
			 ,CreatedByIp
			FROM dbo.RefreshToken
			WHERE UserId = @userId";

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<RefreshTokenEntity>(sql, new { userId }).ToList();

			return lst;
		}

		public RefreshTokenEntity? Select(string token)
		{
			const string sql = @"
			SELECT
				RefreshTokenId
			 ,UserId
			 ,Token
			 ,CreatedOn
			 ,ExpiresOn
			 ,CreatedByIp
			FROM dbo.RefreshToken
			WHERE Token = @token";

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<RefreshTokenEntity>(sql, new { token }).ToList();

			return !lst.Any() ? null : lst.Single();
		}


		public void Insert(RefreshTokenEntity entity)
		{
			const string sql = @"INSERT INTO dbo.RefreshToken (
				RefreshTokenId
			 ,UserId
			 ,Token
			 ,CreatedOn
			 ,ExpiresOn
			 ,CreatedByIp
			) VALUES (
				@RefreshTokenId
			 ,@UserId
			 ,@Token
			 ,@CreatedOn
			 ,@ExpiresOn
			 ,@CreatedByIp);";

			using var connection = new SqlConnection(ConnectionString);

			var p = new DynamicParameters();

			p.Add("@RefreshTokenId", dbType: DbType.Guid, value: entity.RefreshTokenId);
			p.Add("@UserId", dbType: DbType.Int32, value: entity.UserId);
			p.Add("@Token", dbType: DbType.String, value: entity.Token, size: 255);
			p.Add("@CreatedOn", dbType: DbType.DateTime2, value: entity.CreatedOn, size: 3);
			p.Add("@ExpiresOn", dbType: DbType.DateTime2, value: entity.ExpiresOn, size: 3);
			p.Add("@CreatedByIp", dbType: DbType.String, value: entity.CreatedByIp, size: 39);

			connection.Execute(sql, entity);
		}

		public void DeleteExpired(int userId)
		{
			const string sql = @"DELETE FROM dbo.RefreshToken WHERE UserId = @userId AND ExpiresOn <= SYSUTCDATETIME()";

			using var connection = new SqlConnection(ConnectionString);

			connection.Execute(sql, new { userId });
		}

		public void Delete(int userId, string token)
		{
			const string sql = @"DELETE FROM dbo.RefreshToken WHERE UserId = @userId AND Token = @token";

			using var connection = new SqlConnection(ConnectionString);

			connection.Execute(sql, new {userId, token});
		}
	}
}
