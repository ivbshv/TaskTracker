using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace TaskTracker
{
    public partial class Program
    {
        public static class TaskStorage
        {
            private static readonly string FilePath = "tasks.json";

            public static void SaveTasks(List<Task> tasks)
            {
                var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }

            public static List<Task> LoadTasks()
            {
                if (!File.Exists(FilePath))
                    return new List<Task>();

                var json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Task>>(json) ?? new List<Task>();
            }
        }
    }
}
