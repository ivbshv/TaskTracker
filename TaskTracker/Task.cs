using System;

namespace TaskTracker
{
    public partial class Program
    {
        public class Task
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public PriorityLevel Priority { get; set; }
            public bool isCompleted { get; set; } = false;

            public Task(string title, string text)
            {
                if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Заголовок задачи не может быть пустым");
                if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Описание задачи не может быть пустым");

                Title = title;
                Text = text;
            }
        }
    }
}
