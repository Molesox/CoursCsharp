using Dapper;
using Todapp.Data.Context;
using Todapp.Data.Contracts;
using Todapp.Models;

namespace Todapp.Data.Services
{
    public class TodoItemServiceDapper : ITodoItemService
    {
        private readonly SqlContext _context;

        public TodoItemServiceDapper(SqlContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItem todoItem)
        {
            const string query = @"INSERT INTO TodoItems(Description, IsCompleted, UserId, ParentTodoItemId) 
                               VALUES (@Description, @IsCompleted, @UserId, @ParentTodoItemId); 
                               SELECT CAST(SCOPE_IDENTITY() as int);";
           
            using (var conn = _context.CreateConnection())
            {
                var todoItemId = await conn.QuerySingleAsync<int>(query, todoItem);
                todoItem.TodoItemId = todoItemId;
                return todoItem;
            }
        }

        public async Task<bool> DeleteTodoItemAsync(TodoItem todoItem)
        {
            const string query = "DELETE FROM TodoItems WHERE TodoItemId = @TodoItemId";
            using (var conn = _context.CreateConnection())
            {
                var affectedRows = await conn.ExecuteAsync(query, new { todoItem.TodoItemId });
                return affectedRows > 0;
            }
        }

        public async Task<TodoItem[]> GetTodoItemsAsync()
        {
            const string query = "SELECT * FROM TodoItems";
            using (var conn = _context.CreateConnection())
            {
                var todoItems = await conn.QueryAsync<TodoItem>(query);
                return todoItems.ToArray();
            }
        }

        public async Task<TodoItem> UpdateTodoItemAsync(TodoItem todoItem)
        {
            const string query = @"UPDATE TodoItems SET 
                               Description = @Description, 
                               IsCompleted = @IsCompleted, 
                               UserId = @UserId, 
                               ParentTodoItemId = @ParentTodoItemId
                               WHERE TodoItemId = @TodoItemId";
            using (var conn = _context.CreateConnection())
            {
                var affectedRows = await conn.ExecuteAsync(query, todoItem);
                if (affectedRows > 0) return todoItem;
                return null;
            }
        }
    }
}