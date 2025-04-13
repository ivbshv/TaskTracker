using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskTracker
{
    public partial class Program
    {
        public class TaskRepository
        {
            static List<Task> tasks;
            private static int idCounter = 1;

            public TaskRepository()
            {
                tasks = new List<Task>();
            }

            public static int GetNextId()
            {
                return idCounter++;
            }

            public void AddTask(Task task)
            {
                tasks.Add(task);
            }

            public void ShowTasks()
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Вы еще не создали задач для выполнения");
                    return;
                }

                var sortedTasks = tasks
                    .OrderByDescending(t => t.Priority)
                    .ToList();

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

            public  void DeleteTask(int id)
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    tasks.Remove(task);
                    Console.WriteLine($"Задача под номером {id} удалена");
                }
                else
                {
                    Console.WriteLine("Задача с указанным ID не найдена.");
                }
            }

            public  Task FindTask(Func<Task, bool> predicate)
            {
                return tasks.FirstOrDefault(predicate);
            }

            public void UpdateTaskDescription(int id, string newText)
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Список задач пока пуст.");
                    return; 
                }

                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    task.Text = newText;
                    Console.WriteLine($"Описание задачи \"{task.Title}\" обновлено.");
                }
                else
                {
                    Console.WriteLine("Задача с указанным ID не найдена.");
                }
            }

            public void SearchTasksByText(string keyword)
            {
                var foundTasks = tasks
                    .Where(t => t.Text != null && t.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (foundTasks.Count > 0)
                {
                    foreach (var task in foundTasks)
                    {
                        Console.WriteLine($"ID: {task.Id}, {task.Title} - Priority: {task.Priority} - {task.Text}");
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
        }
    }
}
