using Dapper;
using Microsoft.Data.SqlClient;
using Redis.Services.DateBaseModels;
using System.Data;

namespace Redis.Db.Repositories
{
    public class UserRepository
    {
        readonly string? connectionString = null;
        public UserRepository(
            string conn)
        {
            connectionString = conn;
        }
        public User GetUser(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var parameters = new { Id = id };
            string query = "SELECT * FROM dbo.Users WHERE Id = @Id";
            var result = db
                .Query<User>(query, parameters)
                .First();

            return result;
        }
    }
}
