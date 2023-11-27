
using DataLibrary;
using SharedLibrary.Models;

namespace Todapp.Commands
{
    public class AddTodoItemCommand : ICommand
    {
        private readonly ITodoItemService _todoItemService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTodoItemCommand"/> class.
        /// </summary>
        /// <param name="todoItemService">The Todo item service used for adding Todo items.</param>
        public AddTodoItemCommand(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Executes the addition command asynchronously.
        /// </summary>
        /// <remarks>
        /// The method prompts the user to enter a description for the new Todo item. 
        /// It then creates a new Todo item and attempts to add it using the provided ITodoItemService.
        /// If the Todo item is successfully added, a confirmation message is displayed.
        /// If the addition fails or an exception occurs, an error message is displayed.
        /// </remarks>
        public async Task Execute()
        {
            try
            {
                Console.WriteLine("Adding a new Todo Item...");

                Console.Write("Enter Description: ");
                var description = Console.ReadLine();

                var newItem = new TodoItem()
                {
                    UserId = 1,
                    Description = description ?? "Undescripted",
                    IsCompleted = false
                };

                var addedItem = await _todoItemService.TodoItemRepository.Insert(newItem);
                if (addedItem != null)
                {
                    Console.WriteLine("Todo item added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add todo item.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding todo item: {ex.Message}");
            }
        }
    }
}