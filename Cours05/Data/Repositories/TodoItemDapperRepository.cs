using Dapper;
using Microsoft.Data.SqlClient;
using SharedLibrary.Repository;
using System.Data;
using System.Text;
using SharedLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace DataLibrary.Repositories
{
    /// <summary>
    /// Repository for managing TodoItem entities using Dapper.
    /// </summary>
    public class TodoItemDapperRepository : IRepository<TodoItem>
    {
        private readonly IConfiguration _configuration;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TodoItemDapperRepository class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public TodoItemDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a new database connection using the configuration's default connection string.
        /// </summary>
        private IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        /// <summary>
        /// Deletes a TodoItem and its subtasks recursively from the database.
        /// </summary>
        /// <param name="todoItemId">The ID of the TodoItem to delete.</param>
        /// <returns>True if the delete operation was successful, false otherwise.</returns>
        private async Task<bool> DeleteTodoItemWithSubTasksAsync(int todoItemId)
        {
            using var connection = CreateConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var affectedRows = await RecursiveDelete(todoItemId, connection, transaction);
                transaction.Commit();

                return affectedRows > 0;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Recursively deletes subtasks of a TodoItem.
        /// </summary>
        /// <param name="todoItemId">The ID of the TodoItem whose subtasks are to be deleted.</param>
        /// <param name="connection">The database connection.</param>
        /// <param name="transaction">The database transaction.</param>
        private async Task<int> RecursiveDelete(int todoItemId, IDbConnection connection, IDbTransaction transaction)
        {
            // Find subtasks
            var subtasksSql = "SELECT TodoItemId FROM TodoItems WHERE ParentTodoItemId = @TodoItemId";
            var subtaskIds = await connection.QueryAsync<int>(subtasksSql, new { TodoItemId = todoItemId }, transaction);

            //recursive deletion
            foreach (var subtaskId in subtaskIds) await RecursiveDelete(subtaskId, connection, transaction);

            // Delete the actual item
            var sql = "DELETE FROM TodoItems WHERE TodoItemId = @TodoItemId";
            return await connection.ExecuteAsync(sql, new { TodoItemId = todoItemId }, transaction);
        }

        /// <summary>
        /// Determines the SQL operator as a string based on the provided FilterOperator.
        /// </summary>
        /// <param name="filterOperator">The filter operator to be converted to an SQL operator.</param>
        /// <param name="parameterName">The name of the parameter in the SQL query.</param>
        /// <param name="parameters">The collection of dynamic parameters for the query.</param>
        /// <param name="value">The value to be used in the filter.</param>
        /// <returns>The corresponding SQL operator as a string.</returns>
        private string GetSqlOperator(FilterOperator filterOperator, string parameterName, DynamicParameters parameters, object value)
        {
            switch (filterOperator)
            {
                case FilterOperator.Equals:
                    parameters.Add(parameterName, value);
                    return "=" + parameterName;
                case FilterOperator.NotEquals:
                    parameters.Add(parameterName, value);
                    return "<>" + parameterName;
                case FilterOperator.StartsWith:
                    parameters.Add(parameterName, value + "%");
                    return "LIKE " + parameterName;
                case FilterOperator.EndsWith:
                    parameters.Add(parameterName, "%" + value);
                    return "LIKE " + parameterName;
                case FilterOperator.Contains:
                    parameters.Add(parameterName, "%" + value + "%");
                    return "LIKE " + parameterName;
                case FilterOperator.LessThan:
                case FilterOperator.GreaterThan:
                case FilterOperator.LessThanOrEqual:
                case FilterOperator.GreaterThanOrEqual:
                    parameters.Add(parameterName, value);
                    return GetComparisonOperator(filterOperator) + parameterName;
                default:
                    throw new ArgumentException("Invalid FilterOperator");
            }
        }

        /// <summary>
        /// Converts a FilterOperator to its corresponding SQL comparison operator.
        /// </summary>
        /// <param name="filterOperator">The filter operator to be converted.</param>
        /// <returns>The SQL comparison operator as a string.</returns>
        private string GetComparisonOperator(FilterOperator filterOperator)
        {
            return filterOperator switch
            {
                FilterOperator.LessThan => "<",
                FilterOperator.GreaterThan => ">",
                FilterOperator.LessThanOrEqual => "<=",
                FilterOperator.GreaterThanOrEqual => ">=",
                _ => "="
            };
        }

        #endregion

        #region IRepository Implementation

        /// <summary>
        /// Retrieves all TodoItems from the database.
        /// </summary>
        /// <returns>A collection of TodoItems.</returns>
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            var sql = "SELECT * FROM TodoItems";
            using var connection = CreateConnection();
            return await connection.QueryAsync<TodoItem>(sql);
        }

        /// <summary>
        /// Retrieves a TodoItem by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the TodoItem.</param>
        /// <returns>The TodoItem if found, otherwise null.</returns>
        public async Task<TodoItem?> GetByID(object id)
        {
            var sql = "SELECT * FROM TodoItems WHERE TodoItemId = @TodoItemId";
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<TodoItem>(sql, new { TodoItemId = id });
        }

        /// <summary>
        /// Inserts a new TodoItem into the database.
        /// </summary>
        /// <param name="entity">The TodoItem to insert.</param>
        /// <returns>The inserted TodoItem with its new identifier.</returns>
        public async Task<TodoItem?> Insert(TodoItem entity)
        {
            var sql = @"
        INSERT INTO TodoItems (UserId, ParentTodoItemId, Description, IsCompleted)
        VALUES (@UserId, @ParentTodoItemId, @Description, @IsCompleted);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, entity);
            entity.TodoItemId = id;
            return entity;
        }

        /// <summary>
        /// Deletes a TodoItem from the database.
        /// </summary>
        /// <param name="entityToDelete">The TodoItem to delete.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> Delete(TodoItem entityToDelete)
        {
            return await DeleteTodoItemWithSubTasksAsync(entityToDelete.TodoItemId);
        }

        /// <summary>
        /// Deletes a TodoItem by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the TodoItem to delete.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> Delete(object id)
        {
            return await DeleteTodoItemWithSubTasksAsync((int)id);
        }

        /// <summary>
        /// Retrieves TodoItems based on a query filter.
        /// </summary>
        /// <param name="queryFilter">The filter to apply to the query.</param>
        /// <returns>A collection of TodoItems that match the filter.</returns>
        public async Task<IEnumerable<TodoItem>> Get(QueryFilter<TodoItem> queryFilter)
        {
            using var connection = CreateConnection();
            var sqlBuilder = new StringBuilder();

            // Process SELECT clause with IncludePropertyNames
            if (queryFilter.IncludePropertyNames.Any())
            {
                var selectedColumns = string.Join(", ", queryFilter.IncludePropertyNames);
                sqlBuilder.Append($"SELECT {selectedColumns} FROM TodoItems");
            }
            else
            {
                sqlBuilder.Append("SELECT * FROM TodoItems");
            }

            var parameters = new DynamicParameters();

            // Process WHERE clause
            if (queryFilter.FilterProperties.Any())
            {
                sqlBuilder.Append(" WHERE ");
                var whereClauses = new List<string>();

                foreach (var fp in queryFilter.FilterProperties)
                {
                    var propertyName = fp.Name;
                    var parameterName = $"@{propertyName}";
                    string clause = $"{propertyName} {GetSqlOperator(fp.Operator, parameterName, parameters, fp.Value)}";
                    whereClauses.Add(clause);
                }

                sqlBuilder.Append(string.Join(" AND ", whereClauses));
            }

            // Process ORDER BY clause
            if (!string.IsNullOrWhiteSpace(queryFilter.OrderByPropertyName))
            {
                sqlBuilder.Append(" ORDER BY ");
                sqlBuilder.Append(queryFilter.OrderByPropertyName);
                if (queryFilter.OrderByDescending)
                    sqlBuilder.Append(" DESC");
            }

            // Execute the query
            return await connection.QueryAsync<TodoItem>(sqlBuilder.ToString(), parameters);
        }


        /// <summary>
        /// Retrieves TodoItems using a LINQ-based query filter.
        /// </summary>
        /// <param name="linqQueryFilter">The LINQ query filter.</param>
        /// <returns>A collection of TodoItems that match the LINQ query.</returns>
        /// <remarks>
        /// This method retrieves all TodoItems from the database and then applies the LINQ filter.
        /// It may not be efficient for large datasets or complex queries.
        /// </remarks>
        public async Task<IEnumerable<TodoItem>> Get(LinqQueryFilter<TodoItem> linqQueryFilter)
        {
            var allItems = await GetAll();
            return linqQueryFilter.GetFilteredList(allItems.AsQueryable());
        }

        /// <summary>
        /// Updates a TodoItem in the database.
        /// </summary>
        /// <param name="entityToUpdate">The TodoItem to update.</param>
        /// <returns>The updated TodoItem if successful, otherwise null.</returns>
        public async Task<TodoItem?> Update(TodoItem entityToUpdate)
        {
            var sql = @"
        UPDATE TodoItems
        SET UserId = @UserId, 
            ParentTodoItemId = @ParentTodoItemId, 
            Description = @Description, 
            IsCompleted = @IsCompleted
        WHERE TodoItemId = @TodoItemId";

            using var connection = CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, entityToUpdate);
            return affectedRows > 0 ? entityToUpdate : null;
        }

        #endregion

    }
}


