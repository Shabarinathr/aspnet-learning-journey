using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // This represents the "Tasks" table
    public DbSet<TaskItem> Tasks { get; set; }
}

// Task model (moved here from Program.cs)
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }
}
