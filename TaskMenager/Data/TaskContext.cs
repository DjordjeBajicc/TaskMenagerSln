using Microsoft.EntityFrameworkCore;
using TaskMenager.Data.Entities;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<TaskItem>()
    //        //.HasD;  // Uverite se da je ovo postavljeno ako koristite Task entitet
    //}
}
