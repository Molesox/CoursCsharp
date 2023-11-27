using Todapp.Commands;
using Todapp.Todapp.Commands;

namespace Todapp
{
    /// <summary>
    /// Executes command objects based on a string key, utilizing a service provider for command instantiation.
    /// </summary>
    public class CommandExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _commandMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutor"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider used for resolving command dependencies.</param>
        public CommandExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _commandMap = new Dictionary<string, Type>
                            {
                                { "add", typeof(AddTodoItemCommand) },
                                { "list", typeof(ListTodoItemsCommand) },
                                { "add-subtask", typeof(AddSubTaskCommand) },
                                { "delete", typeof(DeleteTodoItemCommand) },
                            };
        }

        /// <summary>
        /// Asynchronously executes a command based on a given string key.
        /// </summary>
        /// <param name="commandKey">The key associated with the command to execute.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <remarks>
        /// If the commandKey does not match any command in the _commandMap, it prints "Invalid command." to the console.
        /// </remarks>
        public async Task ExecuteCommand(string commandKey)
        {
            if (_commandMap.TryGetValue(commandKey, out Type? commandType))
            {
                var command = _serviceProvider.GetService(commandType);

                ICommand? c = command as ICommand;
                if (c is not null) await c.Execute();
            }
            else
            {
                Console.WriteLine("Invalid command.");
            }
        }
    }

}