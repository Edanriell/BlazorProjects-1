using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Data;

public class TaskManagerServerContext : DbContext
{
    public TaskManagerServerContext(DbContextOptions<TaskManagerServerContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> TaskItem { get; set; } = default!;
}