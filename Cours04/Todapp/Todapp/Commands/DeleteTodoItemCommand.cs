using System;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary;
using Microsoft.AspNetCore.Mvc;


namespace Todapp.Commands
{
    /// <summary>
    /// Represents a command to delete a Todo item.
    /// </summary>
    /// <remarks>
    /// This command class is responsible for handling the deletion of a Todo item from a Todo list. 
    /// It utilizes an ITodoItemService to perform the actual deletion operation.
    /// </remarks>
    public class DeleteTodoItemCommand : ICommand
    {
        private readonly ITodoItemService _todoItemService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTodoItemCommand"/> class.
        /// </summary>
        /// <param name="todoItemService">The Todo item service used for deleting Todo items.</param>
        public DeleteTodoItemCommand(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Executes the deletion command asynchronously.
        /// </summary>
        /// <remarks>
        /// The method prompts the user to enter the ID of the Todo item to be deleted. 
        /// It then attempts to delete the Todo item using the provided ITodoItemService.
        /// If the Todo item is successfully deleted, a confirmation message is displayed.
        /// If the Todo item is not found or an exception occurs, an error message is displayed.
        /// </remarks>
        public async Task Execute()
        {
            try
            {
                Console.Write("Enter Todo Item ID to delete: ");
                var todoItemId = int.Parse(Console.ReadLine() ?? "-1");

                if (todoItemId != -1)
                {
                    var result = await _todoItemService.TodoItemRepository.Delete(todoItemId);

                    if (result) Console.WriteLine("Todo item deleted successfully.");
                    else Console.WriteLine("Todo item not found.");
                }
                else
                {
                    Console.WriteLine("Todo item not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting todo item: {ex.Message}");
            }
        }
    }

}
