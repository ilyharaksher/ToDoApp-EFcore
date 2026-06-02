using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp_EFcore.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Task> Tasks => Set<Task>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Tasks.db");
        }
    }
}
