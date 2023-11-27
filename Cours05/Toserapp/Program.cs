using System.CodeDom;
using DataLibrary;
using DataLibrary.Context;
using DataLibrary.Repositories;
using DataLibrary.Services;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using SharedLibrary.Repository;


var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD

//IConfiguration configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .Build();

//var connectionString = configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddSingleton(configuration);

// Register contexts
//builder.Services.AddDbContext<TodappDbContext>(options => options.UseSqlServer(connectionString));
=======

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(configuration);

// Register contexts
builder.Services.AddDbContext<TodappDbContext>(options => options.UseSqlServer(connectionString));
>>>>>>> efce0574c516519d851904d03954e7bab19ebf50

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

<<<<<<< HEAD

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
=======
app.MapGet("/api/todoitems/user/{userId}", async (int userId, ITodoItemService todoItemService) =>
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
});

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
>>>>>>> efce0574c516519d851904d03954e7bab19ebf50
        return Results.Problem(ex.Message);
    }
});



app.Run();
