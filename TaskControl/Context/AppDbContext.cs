using Microsoft.EntityFrameworkCore;
using TaskControl.Models;

namespace TaskControl.Context;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=db_taskControl.db");
    }

    public DbSet<TaskItem> TaskItems { get; set; }
}
