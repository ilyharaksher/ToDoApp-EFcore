using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp_EFcore.Models
{
    internal class TaskTodo
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
