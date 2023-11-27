
using DataLibrary;

namespace Todapp.Commands
{
    /// <summary>
    /// Represents a command to list all Todo items for a specific user.
    /// </summary>
    /// <remarks>
    /// This command class is responsible for handling the retrieval and display of Todo items associated with a specific user.
    /// It uses an ITodoItemService to perform the retrieval operation.
    /// </remarks>
    public class ListTodoItemsCommand : ICommand
    {
        private readonly ITodoItemService _todoItemService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTodoItemsCommand"/> class.
        /// </summary>
        /// <param name="todoItemService">The Todo item service used for retrieving Todo items.</param>
        public ListTodoItemsCommand(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Executes the command asynchronously to retrieve and display Todo items.
        /// </summary>
        /// <remarks>
        /// This method retrieves Todo items for a hardcoded user ID (currently set to 1).
        /// It displays a list of Todo items if any are found, or a message indicating that no items are found otherwise.
        /// In case of an exception, an error message is displayed.
        /// Note: The hardcoding of the user ID may limit the flexibility of this command in different scenarios.
        /// </remarks>
        public async Task Execute()
        {
            try
            {
                var todoItems = await _todoItemService.GetUserTodoItems(1);

                if (todoItems != null && todoItems.Any())
                {
                    Console.WriteLine("Todo Items:");
                    foreach (var item in todoItems)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("-------------");
                    }
                }
                else
                {
                    Console.WriteLine("No todo items found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving todo items: {ex.Message}");
            }
        }
    }
}