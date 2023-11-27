
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Models;
using SharedLibrary.Repository;
using Todapp;

using DataLibrary;
using DataLibrary.Repositories;
using DataLibrary.Services;
using Todapp.Commands;
using DataLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todapp.Todapp.Commands;

Console.WriteLine("Welcom to Todapp !");

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration);

// Register contexts
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
services.AddDbContext<TodappDbContext>(options => options.UseSqlServer(connectionString));

// Register repositories
services.AddScoped<IRepository<TodoItem>, TodoItemDapperRepository>();
services.AddScoped<IRepository<User>, UserADORepository>();

// Register services 
services.AddScoped<ITodoItemService, TodoItemService>();
services.AddScoped<IUserService, UserService>();

// Register commands
services.AddTransient<AddTodoItemCommand>();
services.AddTransient<AddSubTaskCommand>();
services.AddTransient<ListTodoItemsCommand>();
services.AddTransient<DeleteTodoItemCommand>();

var serviceProvider = services.BuildServiceProvider();

// Application logic
var commandExecutor = new CommandExecutor(serviceProvider);

while (true)
{
    Console.WriteLine("\nAvailable Commands: 'add', 'list', 'add-subtask', 'delete', exit'");
    Console.Write("Enter command: ");
    
    var input = Console.ReadLine();

    if (input is null || input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

    await commandExecutor.ExecuteCommand(input);
}
