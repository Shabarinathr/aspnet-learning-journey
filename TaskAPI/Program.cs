using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var taskList = new List<TaskItem>
{
    new TaskItem {Id = 1, Title = "Learn ASP.net Core", IsCompleted = false},
    new TaskItem {Id = 2, Title = "Build Task API",IsCompleted = false},
    new TaskItem {Id = 3, Title = "Pass campus Placement", IsCompleted = false},
};
//Get any one task
app.MapGet("/tasks",()=> taskList);
app.MapGet("/tasks/{id}",(int id)=>
{
    var task = taskList.FirstOrDefault(t=> t.Id == id);
    return task is not null? Results.Ok(task) : Results.NotFound();
});

//create new task(POST)
app.MapPost("/tasks", (TaskItem newTask)=>
{
    newTask.Id = taskList.Any() ? taskList.Max(t=> t.Id) +1 : 1;
    taskList.Add(newTask);
    return Results.Created($"/tasks/{newTask.Id}", newTask);
});

//update the existing task
app.MapPut("/tasks/{id}", (int id, TaskItem updatedTask)=>
{
    var task = taskList.FirstOrDefault(t => t.Id == id);
    if(task is null) return Results.NotFound();

    task.Title = updatedTask.Title;
    task.IsCompleted = updatedTask.IsCompleted;
    return Results.Ok(task);
});

app.MapDelete("/tasks/{id}", (int id) =>
{
    var task = taskList.FirstOrDefault(t => t.Id == id);
    if (task is null) return Results.NotFound();
    
    taskList.Remove(task);
    return Results.NoContent();
});

app.Run();
class TaskItem
{
    public int Id{get; set;}
    public string Title{get; set;}="";
    public bool IsCompleted{get; set;}
}