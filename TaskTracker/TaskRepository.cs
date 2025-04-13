using static TaskTracker.Program;
using System.Collections.Generic;
using System;
using TaskTracker;
using System.Linq;

public class TaskRepository
{
    private List<Task> tasks;
    private static int idCounter = 1; 

    public TaskRepository()
    {
        tasks = new List<Task>();

        LoadTasksFromFile();

        if (tasks.Count > 0)
        {
            idCounter = tasks.Max(t => t.Id) + 1;
        }
    }

    public int GetNextId()
    {
        return idCounter++;
    }

    public void AddTask(Task task)
    {
        task.Id = GetNextId(); // Назначаем уникальный ID
        tasks.Add(task);
        TaskStorage.SaveTasks(tasks);  // Сохраняем задачи в файл
    }

    public void ShowTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Вы еще не создали задач для выполнения");
            return;
        }

        var sortedTasks = tasks.OrderByDescending(t => t.Priority).ToList();

        foreach (var task in sortedTasks)
        {
            string priorityDescription = PriorityHelper.GetPriorityDescription(task.Priority);
            Console.WriteLine($"{task.Id}, {task.Title} - Приоритет: {priorityDescription} - {(task.isCompleted ? "Выполнена" : "Ожидает выполнения")}");
            Console.WriteLine($"Описание: {task.Text}\n");
        }
    }

    public void TaskDone(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            if (!task.isCompleted)
            {
                task.isCompleted = true;
                Console.WriteLine($"Задача \"{task.Title}\" отмечена как выполненная.");
                TaskStorage.SaveTasks(tasks);  // Сохраняем изменения
            }
            else
            {
                Console.WriteLine("Задача уже была выполнена.");
            }
        }
        else
        {
            Console.WriteLine("Задача с указанным ID не найдена.");
        }
    }

    public void DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
            Console.WriteLine($"Задача под номером {id} удалена");
            TaskStorage.SaveTasks(tasks);  // Сохраняем изменения
        }
        else
        {
            Console.WriteLine("Задача с указанным ID не найдена.");
        }
    }

    public void UpdateTaskDescription(int id, string newText)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.Text = newText;
            Console.WriteLine($"Описание задачи \"{task.Title}\" обновлено.");
            TaskStorage.SaveTasks(tasks);  // Сохраняем изменения
        }
        else
        {
            Console.WriteLine("Задача с указанным ID не найдена.");
        }
    }

    public void SearchTasksByText(string keyword)
    {
        var foundTasks = tasks.Where(t => t.Text != null && t.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        if (foundTasks.Count > 0)
        {
            foreach (var task in foundTasks)
            {
                Console.WriteLine($"ID: {task.Id}, {task.Title} - Приоритет: {task.Priority} - {task.Text}");
            }
        }
        else
        {
            Console.WriteLine("Задачи с таким описанием не найдены.");
        }
    }

    public void DoneStatisticViewer()
    {
        var doneTasks = tasks.Count(t => t.isCompleted);
        Console.WriteLine($"Выполнено {doneTasks} задач");
    }

    private void LoadTasksFromFile()
    {
        tasks = TaskStorage.LoadTasks();  // Загружаем задачи из файла
    }
}
