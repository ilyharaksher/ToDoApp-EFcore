using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


//Console.WriteLine(Directory.GetCurrentDirectory());

bool run = true;

while (run)
{
    Console.WriteLine("Выберите действие:\n" +
        "1. Показать задачи\n" +
        "2. Добавить задачу\n" +
        "3. Переименовать задачу\n" +
        "4. Удалить задачу\n" +
        "5. Отметить задачу выполненной\n" +
        "6. Показать выполненные задачи\n" +
        "7. Показать невыполненные задачи \n" +
        "0. Закончить работу\n");
    int choice = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    if (choice == 1) // Показать всё
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var tasks = db.Tasks.ToList();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Id: {task.Id} Название: {task.Name} Статус: {(task.IsCompleted ? "Выполнено" : "Не выполнено")}");
            }
        }
        Console.WriteLine();
    }

    else if (choice == 2) // Добавить задачу
    {
        Console.WriteLine("Введите название новой задачи");
        string? newTaskName = Console.ReadLine();

        using (ApplicationContext db = new ApplicationContext())
        {
            Task newTask = new Task { Name = newTaskName };
            db.Tasks.Add(newTask);
            db.SaveChanges();
        }
        Console.WriteLine();
    }

    else if (choice == 3)  // Переименовать задачу
    {
        Console.WriteLine("Введите Id задачи, которую хотите переименовать");
        int updatingId = Convert.ToInt32(Console.ReadLine());

        using (ApplicationContext db = new ApplicationContext())
        {
            Task? updatingTask = db.Tasks.Find(updatingId);
            if (updatingTask != null)
            {
                Console.WriteLine("Введите новое название");
                string updatingTaskNewName = Console.ReadLine();
                updatingTask.Name = updatingTaskNewName;
                db.Tasks.Update(updatingTask);
                db.SaveChanges();
                Console.WriteLine("Задача успешно обновлена");
            }
            else
            {
                Console.WriteLine("Задачи с таким Id нет");
            }
        }
        Console.WriteLine();
    }
    else if (choice == 4) // удалить задачу
    {
        Console.WriteLine("Введите ID удаляемой задачи");
        int removing_id = Convert.ToInt32(Console.ReadLine());

        using (ApplicationContext db = new ApplicationContext())
        {
            Task? removingTask = db.Tasks.Find(removing_id);

            if (removingTask != null)
            {
                db.Remove(removingTask);
                db.SaveChanges();
                Console.WriteLine("Задача успешно удалена!");
            }
            else
            {
                Console.WriteLine($"Задачи с Id {removing_id} нет");
            }
            Console.WriteLine();
        }
    }

    else if (choice == 5) // Отметить задачу выполненной
    {
        Console.WriteLine("Введите Id задачи, которую надо отметить выполненной");
        int taskId = Convert.ToInt32(Console.ReadLine());
        using (ApplicationContext db = new ApplicationContext())
        {
            Task? taskToComplete = db.Tasks.Find(taskId);
            if (taskToComplete != null)
            {
                taskToComplete.IsCompleted = true;
                db.SaveChanges();
                Console.WriteLine($"{taskToComplete.Name} отмечена выполненной");
            }

            else
            {
                Console.WriteLine($"Задачи с Id {taskId} нет");
            }
        }
        Console.WriteLine();
    }

    else if (choice == 6) // показать только выполненные

    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var tasks = db.Tasks.Where(t => t.IsCompleted == true).ToList();
            if (tasks.Count() > 0)
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Name}");
                }
            }
            else
            {
                Console.WriteLine("Выполненных задач нет.");
            }
            Console.WriteLine();
        }
    }

    else if (choice == 7) // показать только невыполненные
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var tasks = db.Tasks.Where(t => t.IsCompleted == false).ToList();
            if (tasks.Count() > 0)
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Name}");
                }
            }
            else
            {
                Console.WriteLine("Не выполненных задач нет.");
            }
            Console.WriteLine();
        }
    }

    else if (choice == 0)
    {
        Console.WriteLine("Работа завершена! Для выхода нажмите ENTER");
        Console.ReadLine();
        run = false;
    }
}



public class ApplicationContext : DbContext
{
    public DbSet<Task> Tasks => Set<Task>();
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Tasks.db");
    }
}

public class Task
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
