
using ToDo_App_M324.Application;

namespace ToDo_App_M324.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton(new ToDoManager("ToDos.csv"));
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapGet("/todos", (ToDoManager toDoManager) =>
        {
            var todos = toDoManager.GetToDos();
            return todos;
        })
        .WithName("GetAllToDos")
        .WithOpenApi();

        app.MapGet("/todos/{number}", (int number, ToDoManager toDoManager) =>
        {
            var todo = toDoManager.GetToDo(number);
            return todo;
        })
        .WithName("GetToDo")
        .WithOpenApi();

        app.MapDelete("/todos/delete", (ToDoManager toDoManager) =>
        {
            var todos = toDoManager.GetToDos();
            toDoManager.DeleteAllToDos(todos);
            toDoManager.SaveToDos(todos);
        })
        .WithName("DeleteAll")
        .WithOpenApi();

        app.MapDelete("/todos/{number}/delete", (int number, ToDoManager toDoManager) =>
        {
            var todos = toDoManager.GetToDos();
            toDoManager.DeleteToDo(todos, number);
            toDoManager.SaveToDos(todos);
        })
        .WithName("DeleteToDo")
        .WithOpenApi();

        app.MapPost("todos/add", (string text, ToDoManager toDoManager) => 
        {
            var todos = toDoManager.GetToDos();
            toDoManager.AddToDo(todos, text);
            toDoManager.SaveToDos(todos);
        })
        .WithName("AddToDo")
        .WithOpenApi();

        app.Run();
    }
}
