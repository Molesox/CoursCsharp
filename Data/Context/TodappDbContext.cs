using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace DataLibrary.Context
{
    public partial class TodappDbContext : DbContext
    {
        public TodappDbContext()
        {
        }

        public TodappDbContext(DbContextOptions<TodappDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set up the relationship between User and TodoItem
            modelBuilder.Entity<User>()
                .HasMany(u => u.TodoItems)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascading delete from User to TodoItem

            // Set up the self-referencing relationship in TodoItem for subtasks
            modelBuilder.Entity<TodoItem>()
                .HasMany(t => t.SubTasks)
                .WithOne(t => t.ParentTodoItem)
                .HasForeignKey(t => t.ParentTodoItemId)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete in the database

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}