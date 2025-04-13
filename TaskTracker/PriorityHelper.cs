namespace TaskTracker
{
    public static class PriorityHelper
    {
        public static string GetPriorityDescription(PriorityLevel priority)
        {
            switch (priority)
            {
                case PriorityLevel.Low:
                    return "Низкий";
                case PriorityLevel.Medium:
                    return "Средний";
                case PriorityLevel.High:
                    return "Высокий";
                default:
                    return "Неизвестно";
            }
        }
    }
}
