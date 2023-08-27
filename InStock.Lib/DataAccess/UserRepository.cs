using System.Data;
using Dapper;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using Microsoft.Data.SqlClient;

namespace InStock.Lib.DataAccess
{
	public class UserRepository
		: BaseRepository, IUserRepository
	{
		//Had to force this constructor on DI because it kept using the empty constructor instead
		public UserRepository(IAppConfiguration configuration)
			: base(configuration)
		{
			
		}

		public UserEntity? Select(int userId)
		{
			const string sql = @"
			SELECT
				UserId,
				Name,
				Username,
				CreateOnUtc
			FROM dbo.[User]
			WHERE UserId = @UserId";

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<UserEntity>(sql, new { UserId = userId }).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}

		public IEnumerable<UserEntity> SelectAll()
		{
			const string sql = @"
			SELECT
				UserId,
				Name,
				Username,
				CreateOnUtc
			FROM dbo.[User]";

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<UserEntity>(sql).ToList();

			return lst;
		}

		public int Insert(UserEntity entity)
		{
			const string sql = @"INSERT INTO dbo.[User] (
				Name
			) VALUES (
				@Name);

			SELECT SCOPE_IDENTITY() AS PK;";

			using var connection = new SqlConnection(ConnectionString);

			var p = new DynamicParameters();

			p.Add(
				"@Name",
				dbType: DbType.String,
				value: entity.Name,
				size: 255);

			return connection.ExecuteScalar<int>(sql, entity);
		}

		public void Update(UserEntity entity)
		{
			const string sql = @"UPDATE dbo.[User] SET 
				Name = @Name,
				CreateOnUtc = @CreateOnUtc
			WHERE UserId = @UserId";

			using var connection = new SqlConnection(ConnectionString);

			var p = new DynamicParameters();
			p.Add("@UserId", dbType: DbType.Int32, value: entity.UserId);

			p.Add(
				"@Name",
				dbType: DbType.String,
				value: entity.Name,
				size: 255);

			connection.Execute(sql, p);
		}

		public UserEntity? Select(string username)
		{
			const string sql = @"
			SELECT
				UserId,
				Name,
				Username,
				Password,
				IsAllowed
			FROM dbo.[User]
			WHERE Username = @username";

			using var connection = new SqlConnection(ConnectionString);

			var lst = connection.Query<UserEntity>(sql, new { username }).ToList();

			if (!lst.Any()) return null;

			var entity = lst.Single();

			return entity;
		}
	}
}
