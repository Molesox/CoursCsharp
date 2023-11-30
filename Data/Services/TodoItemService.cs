using Microsoft.EntityFrameworkCore;
using SharedLibrary.Repository;
using SharedLibrary.Models;

namespace DataLibrary.Services
{
    /// <summary>
    /// Provides services for managing TodoItem entities, interfacing with TodoItem and User repositories.
    /// </summary>
    public class TodoItemService : ITodoItemService
    {
        private readonly IRepository<TodoItem> _todoItemRepo;
        private readonly IRepository<User> _userRepo;

        /// <summary>
        /// Get the TodoItem Repository
        /// </summary>
        public IRepository<TodoItem> TodoItemRepository => _todoItemRepo;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TodoItemService class, injecting dependencies for TodoItem and User repositories.
        /// </summary>
        /// <param name="todoItemRepo">The repository for TodoItem entities.</param>
        /// <param name="userRepo">The repository for User entities.</param>
        public TodoItemService(IRepository<TodoItem> todoItemRepo, IRepository<User> userRepo)
        {
            _todoItemRepo = todoItemRepo;
            _userRepo = userRepo;
        }


        #endregion

        #region Interface Implementation

        /// <summary>
        /// Retrieves TodoItems for a specific user.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>A collection of TodoItems belonging to the specified user.</returns>
        public async Task<IEnumerable<TodoItem>> GetUserTodoItems(User user)
        {
            return await GetUserTodoItems(user.UserId);
        }

        /// <summary>
        /// Retrieves TodoItems for a specific user, including their subtasks properly incrustated.
        /// </summary>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>A collection of TodoItems belonging to the specified user.</returns>
        public async Task<IEnumerable<TodoItem>> GetUserTodoItems(int userId)
        {
            // Retrieve all TodoItems for the user
            var allUserTodoItems = await _todoItemRepo.Get(new QueryFilter<TodoItem>
            {
                FilterProperties = new List<FilterProperty> { new FilterProperty
                    {
                        Name = nameof(User.UserId),
                        Operator = FilterOperator.Equals,
                        Value=userId.ToString(),
                    }
                }
            });

            // Separate parent items and subtasks
            var parentItems = allUserTodoItems
                .Where(item => item.ParentTodoItemId == null);

            var subTasks = allUserTodoItems
                .Where(item => item.ParentTodoItemId != null);

            // Assign subtasks to their parent items
            foreach (var item in parentItems)
            {
                item.SubTasks = subTasks
                    .Where(st => st.ParentTodoItemId == item.TodoItemId)
                    .ToList();
            }

            return parentItems;
        }

        #endregion
    }

}
