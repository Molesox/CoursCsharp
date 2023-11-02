 
using Microsoft.Extensions.Configuration;
using Todapp.Data.Context;
using Todapp.Data.Services;
using Todapp.Models;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional:false)
    .Build();


var context = new SqlContext(configuration);
var userService = new UserServiceDapper(context);
var todoItemService = new TodoItemServiceDapper(context);


var newUser = new User { UserName = "Nicolas" };

newUser = await userService.AddUserAsync(newUser);
Console.WriteLine($"User Created: {newUser.UserName} with ID {newUser.UserId}");
 
var todo1 = new TodoItem { Description = "Buy Milk", IsCompleted = false, UserId = newUser.UserId };
var todo2 = new TodoItem { Description = "Walk the Dog", IsCompleted = false, UserId = newUser.UserId };

todo1 = await todoItemService.AddTodoItemAsync(todo1);
todo2 = await todoItemService.AddTodoItemAsync(todo2);

Console.WriteLine($"Todo Item 1 Created: {todo1.Description} with ID {todo1.TodoItemId}");
Console.WriteLine($"Todo Item 2 Created: {todo2.Description} with ID {todo2.TodoItemId}");

newUser.UserName = "Nicolò";
newUser = await userService.UpdateUserAsync(newUser);
Console.WriteLine($"User updated: {newUser.UserName} with ID {newUser.UserId}");

await userService.DeleteUserAsync(newUser);







