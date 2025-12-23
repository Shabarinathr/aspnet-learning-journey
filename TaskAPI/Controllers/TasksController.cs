using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;

namespace TaskAPI.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db)
    {
        _db = db;
    }

    // GET /tasks
    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetTasks()
    {
        return await _db.Tasks.ToListAsync();
    }

    // GET /tasks/1
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTask(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();
        return task;
    }

    // POST /tasks
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask(TaskItem newTask)
    {
        _db.Tasks.Add(newTask);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask);
    }

    // PUT /tasks/1
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTask(int id, TaskItem updatedTask)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        task.Title = updatedTask.Title;
        task.IsCompleted = updatedTask.IsCompleted;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /tasks/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
