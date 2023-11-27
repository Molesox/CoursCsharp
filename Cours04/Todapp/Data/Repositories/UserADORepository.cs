using Microsoft.Data.SqlClient;
using SharedLibrary.Repository;
using SharedLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Repositories
{
    /// <summary>
    /// A repository for managing User entities using ADO.NET.
    /// </summary>
    public class UserADORepository : IRepository<User>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserADORepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object containing connection string information.</param>
        public UserADORepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Interface Implementation

        /// <summary>
        /// Deletes a User entity by its ID.
        /// </summary>
        /// <param name="entityToDelete">The User entity to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public async Task<bool> Delete(User entityToDelete)
        {
            var sql = "DELETE FROM Users WHERE UserId = @UserId";
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", entityToDelete.UserId);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                    return affectedRows > 0;
                }
            }
        }

        /// <summary>
        /// Deletes a User entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the User entity to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public async Task<bool> Delete(object id)
        {
            var sql = "DELETE FROM Users WHERE UserId = @UserId";
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                    return affectedRows > 0;
                }
            }
        }

        /// <summary>
        /// Retrieves all User entities from the database.
        /// </summary>
        /// <returns>A list of User entities.</returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            var sql = "SELECT UserId, UserName FROM Users";
            var users = new List<User>();
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                        });
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// Retrieves a User entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the User entity to retrieve.</param>
        /// <returns>The User entity if found, otherwise null.</returns>
        public async Task<User?> GetByID(object id)
        {
            var sql = "SELECT UserId, UserName FROM Users WHERE UserId = @UserId";
            User? user = null;
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                // Initialize other properties
                            };
                        }
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Inserts a new User entity into the database.
        /// </summary>
        /// <param name="entity">The User entity to insert.</param>
        /// <returns>The inserted User entity with its updated ID.</returns>
        public async Task<User?> Insert(User entity)
        {
            var sql = "INSERT INTO Users (UserName) OUTPUT INSERTED.UserId VALUES (@UserName)";
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserName", entity.UserName);
                    entity.UserId = (int)await command.ExecuteScalarAsync();
                    return entity;
                }
            }
        }

        /// <summary>
        /// Updates an existing User entity in the database.
        /// </summary>
        /// <param name="entityToUpdate">The User entity to update.</param>
        /// <returns>The updated User entity if the update was successful, otherwise null.</returns>
        public async Task<User?> Update(User entityToUpdate)
        {
            var sql = "UPDATE Users SET UserName = @UserName WHERE UserId = @UserId";
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserName", entityToUpdate.UserName);
                    command.Parameters.AddWithValue("@UserId", entityToUpdate.UserId);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows > 0)
                    {
                        return entityToUpdate;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and returns a new SqlConnection using the configured connection string.
        /// </summary>
        /// <returns>A SqlConnection object.</returns>
        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #endregion

        #region Filtering (Not Implemented)

        public Task<IEnumerable<User>> Get(QueryFilter<User> queryFilter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get(LinqQueryFilter<User> linqQueryFilter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
