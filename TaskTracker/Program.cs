using System;
using System.Linq;

namespace TaskTracker
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            TaskRepository taskRepo = new TaskRepository();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать все задачи");
                Console.WriteLine("3. Отметить задачу как выполненную");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Обновить описание задачи");
                Console.WriteLine("6. Поиск задач по описанию");
                Console.WriteLine("7. Показать статистику по выполненным задачам");
                Console.WriteLine("8. Выход");
                Console.Write("\nВыберите опцию: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Добавление задачи
                        Console.Write("Введите заголовок задачи: ");
                        string title = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(title))
                        {
                            Console.WriteLine("Заголовок не может быть пустым. Нажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Введите описание задачи: ");
                        string description = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(description))
                        {
                            Console.WriteLine("Описание не может быть пустым. Нажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Выберите приоритет (1 - Low, 2 - Medium, 3 - High). Нажмите Enter, чтобы выбрать Low по умолчанию:");
                        string priorityInput = Console.ReadLine();
                        PriorityLevel priority = PriorityLevel.Low;

                        if (!string.IsNullOrWhiteSpace(priorityInput))
                        {
                            if (int.TryParse(priorityInput, out int priorityChoice) && priorityChoice >= 1 && priorityChoice <= 3)
                            {
                                priority = (PriorityLevel)priorityChoice;
                            }
                            else
                            {
                                Console.WriteLine("Некорректный приоритет. Установлен Low по умолчанию.");
                            }
                        }

                        taskRepo.AddTask(new Task(title, description) { Priority = priority });
                        Console.WriteLine("Задача добавлена! Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "2":
                        // Показать все задачи
                        taskRepo.ShowTasks();
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "3":
                        // Отметить задачу как выполненную
                        int taskIdToMark;
                        Console.Write("Введите ID задачи, которую хотите отметить как выполненную: ");
                        while (!int.TryParse(Console.ReadLine(), out taskIdToMark))
                        {
                            Console.WriteLine("Неверный формат ID. Пожалуйста, введите целое число.");
                        }
                        taskRepo.TaskDone(taskIdToMark);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "4":
                        // Удалить задачу
                        int taskIdToDelete;
                        Console.Write("Введите ID задачи, которую хотите удалить: ");
                        while (!int.TryParse(Console.ReadLine(), out taskIdToDelete))
                        {
                            Console.WriteLine("Неверный формат ID. Пожалуйста, введите целое число.");
                        }
                        taskRepo.DeleteTask(taskIdToDelete);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "5":
                        // Обновить описание задачи
                        int taskIdToUpdate;
                        Console.Write("Введите ID задачи для обновления: ");
                        while (!int.TryParse(Console.ReadLine(), out taskIdToUpdate))
                        {
                            Console.WriteLine("Неверный формат ID. Пожалуйста, введите целое число.");
                        }
                        Console.Write("Введите новое описание задачи: ");
                        string newDescription = Console.ReadLine();
                        taskRepo.UpdateTaskDescription(taskIdToUpdate, newDescription);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "6":
                        // Поиск задач по описанию
                        Console.Write("Введите ключевое слово для поиска по описанию задачи: ");
                        string keyword = Console.ReadLine();
                        taskRepo.SearchTasksByText(keyword);
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "7":
                        // Статистика по выполненным задачам
                        taskRepo.DoneStatisticViewer();
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;

                    case "8":
                        // Выход
                        Console.WriteLine("Выход из программы...");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
