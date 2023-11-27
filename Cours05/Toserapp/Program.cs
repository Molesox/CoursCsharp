using DataLibrary;
using DataLibrary.Repositories;
using DataLibrary.Services;
using SharedLibrary.Models;
using SharedLibrary.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRepository<TodoItem>, TodoItemDapperRepository>(); 
builder.Services.AddScoped<IRepository<User>, UserADORepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/api/todoitems/user/{userId}",
    async  (int userId, ITodoItemService todoItemService) =>
            {
                try
                {
                    var todoItems = await todoItemService.GetUserTodoItems(userId);
                    return Results.Ok(todoItems);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }
);

app.MapDelete("/api/todoitems/{todoItemID}", async (int todoItemID, ITodoItemService todoItemService) =>
{
    try
    {
        var result = await todoItemService.TodoItemRepository.Delete(todoItemID);
        if (!result)
            return Results.Problem("Deletion failed.");
        else
            return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});



app.Run();
