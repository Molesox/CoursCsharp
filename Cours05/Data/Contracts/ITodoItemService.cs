using SharedLibrary.Repository;
using SharedLibrary.Models;

namespace DataLibrary
{
    public interface ITodoItemService
    {
        IRepository<TodoItem> TodoItemRepository { get; }
        Task<IEnumerable<TodoItem>> GetUserTodoItems(int userId);
        Task<IEnumerable<TodoItem>> GetUserTodoItems(User user);
    }
}
