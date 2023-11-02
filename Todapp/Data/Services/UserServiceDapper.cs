using Dapper;
using Todapp.Data.Context;
using Todapp.Data.Contracts;
using Todapp.Models;

namespace Todapp.Data.Services
{

    public class UserServiceDapper : IUserService
    {
        private readonly DapperContext _context;

        public UserServiceDapper(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            const string query = "INSERT INTO Users(UserName) VALUES (@UserName); SELECT CAST(SCOPE_IDENTITY() as int);";
            using (var conn = _context.CreateConnection())
            {
                var userId = await conn.QuerySingleAsync<int>(query, new { user.UserName });
                user.UserId = userId;
                return user;
            }
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            const string query = "DELETE FROM Users WHERE UserId = @UserId";
            using (var conn = _context.CreateConnection())
            {
                var affectedRows = await conn.ExecuteAsync(query, new { user.UserId });
                return affectedRows > 0;
            }
        }

        public async Task<User[]> GetUsersAsync()
        {
            const string query = "SELECT * FROM Users";
            using (var conn = _context.CreateConnection())
            {
                var users = await conn.QueryAsync<User>(query);
                return users.ToArray();
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            const string query = "UPDATE Users SET UserName = @UserName WHERE UserId = @UserId";
            using (var conn = _context.CreateConnection())
            {
                var affectedRows = await conn.ExecuteAsync(query, new { user.UserName, user.UserId });
                if (affectedRows > 0)
                    return user;
                return null;
            }
        }
    }
}