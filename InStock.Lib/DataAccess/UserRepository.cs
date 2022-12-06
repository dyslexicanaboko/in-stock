using Dapper;
using InStock.Lib.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InStock.Lib.DataAccess
{
	public class UserRepository
		: BaseRepository, IRepository<UserEntity>
	{
		public UserEntity Select(int userId)
		{
			var sql = @"
			SELECT
				UserId,
				Name,
				CreateOnUtc
			FROM dbo.User
			WHERE UserId = @UserId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<UserEntity>(sql, new { UserId = userId }).ToList();

				if (!lst.Any()) return null;

				var entity = lst.Single();

				return entity;
			}
		}

		public IEnumerable<UserEntity> SelectAll()
		{
			var sql = @"
			SELECT
				UserId,
				Name,
				CreateOnUtc
			FROM dbo.User";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var lst = connection.Query<UserEntity>(sql).ToList();

				return lst;
			}
		}

		
		public int Insert(UserEntity entity)
		{
			var sql = @"INSERT INTO dbo.User (
				Name,
				CreateOnUtc
			) VALUES (
				@Name,
				@CreateOnUtc);

			SELECT SCOPE_IDENTITY() AS PK;";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@Name", dbType: DbType.String, value: entity.Name, size: 255);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

				return connection.ExecuteScalar<int>(sql, entity);
			}
		}

		public void Update(UserEntity entity)
		{
			var sql = @"UPDATE dbo.User SET 
				Name = @Name,
				CreateOnUtc = @CreateOnUtc
			WHERE UserId = @UserId";

			using (var connection = new SqlConnection(ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add(name: "@UserId", dbType: DbType.Int32, value: entity.UserId);
				p.Add(name: "@Name", dbType: DbType.String, value: entity.Name, size: 255);
				p.Add(name: "@CreateOnUtc", dbType: DbType.DateTime2, value: entity.CreateOnUtc, scale: 0);

				connection.Execute(sql, p);
			}
		}
	}
}
