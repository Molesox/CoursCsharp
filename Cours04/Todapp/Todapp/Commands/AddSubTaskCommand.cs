using System;
using System.Threading.Tasks;
using DataLibrary;
using SharedLibrary.Models;
using SharedLibrary.Repository;


namespace Todapp
{

    namespace Todapp.Commands
    {
        /// <summary>
        /// Represents a command to add a new subtask to an existing Todo item.
        /// </summary>
        /// <remarks>
        /// This command class is responsible for handling the addition of a new subtask under a specific Todo item in a Todo list.
        /// It uses an ITodoItemService for performing the operation of adding a subtask.
        /// </remarks>
        public class AddSubTaskCommand : ICommand
        {
            private readonly ITodoItemService _todoItemService;

            /// <summary>
            /// Initializes a new instance of the <see cref="AddSubTaskCommand"/> class.
            /// </summary>
            /// <param name="todoItemService">The Todo item service used for adding subtasks.</param>
            public AddSubTaskCommand(ITodoItemService todoItemService)
            {
                _todoItemService = todoItemService;
            }

            /// <summary>
            /// Executes the addition command for a subtask asynchronously.
            /// </summary>
            /// <remarks>
            /// The method prompts the user to enter the ID of the parent Todo item and a description for the new subtask.
            /// It verifies that the parent Todo item exists and is not already a subtask.
            /// A new subtask is created and added using the provided ITodoItemService.
            /// Confirmation or error messages are displayed based on the success of the addition.
            /// </remarks>
            public async Task Execute()
            {
                try
                {
                    Console.Write("Enter Parent Todo Item ID: ");
                    int parentTodoItemId = int.Parse(Console.ReadLine() ?? "-1");

                    if (parentTodoItemId == -1)
                    {
                        Console.Write("Error adding subtask input isn't an id. ");
                        return;
                    }

                    var parents = await _todoItemService.TodoItemRepository
                    .Get(new LinqQueryFilter<TodoItem>(t => t.TodoItemId == parentTodoItemId));

                    var parent = parents.FirstOrDefault();
                    if (parent is null || parent.ParentTodoItemId.HasValue)
                    {
                        Console.Write("Error adding subtask. Only one parent is allowed or parent not found. ");
                        return;
                    }

                    Console.Write("Enter Subtask Description: ");
                    string description = Console.ReadLine() ?? "Undescripted";

                    // Create and add subtask
                    var subTask = new TodoItem()
                    {
                        UserId = 1,
                        Description = description,
                        IsCompleted = false,
                        ParentTodoItemId = parentTodoItemId
                    };

                    var addedSubTask = await _todoItemService.TodoItemRepository.Insert(subTask);

                    if (addedSubTask is not null) Console.WriteLine("Subtask added successfully.");
                    else Console.WriteLine("Failed to add subtask.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding subtask: {ex.Message}");
                }
            }
        }
    }

}