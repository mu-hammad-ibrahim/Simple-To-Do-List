using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace To_Do_List.DAL.Models.Context
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext() : base()
        {
        }

        public ToDoListContext(DbContextOptions<ToDoListContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Tasks> tasks { get; set; }

    }
}
