using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp_EFcore.Models;

namespace ToDoApp_EFcore.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<TaskTodo> Tasks => Set<TaskTodo>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Tasks.db");
        }
    }
}
