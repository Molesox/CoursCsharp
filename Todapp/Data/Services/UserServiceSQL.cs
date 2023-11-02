using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Todapp.Data.Context;
using Todapp.Data.Contracts;
using Todapp.Models;


namespace Todapp.Data.Services
{

    public class UserServiceSql : IUserService
    {
        private readonly SqlContext _context;

        public UserServiceSql(SqlContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            const string query = "INSERT INTO Users(UserName) OUTPUT INSERTED.UserId VALUES (@UserName);";
            using (var conn = _context.CreateConnection())
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    user.UserId = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return user;
        }


        public async Task<bool> DeleteUserAsync(User user)
        {
            const string deleteTodoItemsQuery = "DELETE FROM TodoItems WHERE UserId = @UserId";
            const string deleteUserQuery = "DELETE FROM Users WHERE UserId = @UserId";

            using (var conn = _context.CreateConnection())
            {
                await conn.OpenAsync();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {

                        using (var cmd = new SqlCommand(deleteTodoItemsQuery, conn, transaction))
                        {
                            cmd.Parameters.Add(new SqlParameter("@UserId", user.UserId));
                            await cmd.ExecuteNonQueryAsync();
                        }

                        // Then delete the User
                        using (var cmd = new SqlCommand(deleteUserQuery, conn, transaction))
                        {
                            cmd.Parameters.Add(new SqlParameter("@UserId", user.UserId));
                            int affectedRows = await cmd.ExecuteNonQueryAsync();


                            transaction.Commit();

                            return affectedRows > 0;
                        }
                    }
                    catch (Exception)
                    {

                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<User[]> GetUsersAsync()
        {
            const string query = "SELECT * FROM Users";
            var users = new List<User>();
            using (var conn = _context.CreateConnection())
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName"))
                        });
                    }
                }
            }
            return users.ToArray();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            const string query = "UPDATE Users SET UserName = @UserName WHERE UserId = @UserId";
            using (var conn = _context.CreateConnection())
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@UserId", user.UserId));
                    int affectedRows = await cmd.ExecuteNonQueryAsync();
                    return affectedRows > 0 ? user : null;
                }
            }
        }
    }
}