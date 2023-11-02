using Todapp.Models;

namespace Todapp.Data.Contracts
{
    public interface ITodoItemService
    {
        public Task<TodoItem[]> GetTodoItemsAsync();
        public Task<TodoItem> AddTodoItemAsync(TodoItem todoItem);
        public Task<bool> DeleteTodoItemAsync(TodoItem todoItem);
        public Task<TodoItem> UpdateTodoItemAsync(TodoItem todoItem);
    }
}