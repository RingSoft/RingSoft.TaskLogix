namespace RingSoft.TaskLogix.DataAccess
{
    public class TaskLookup
    {
        public string Subject { get; set; }

        public DateTime DueDate { get; set; }
    }

    public class TaskRecurDailyLookup
    {
        public string Task { get; set; }

        public byte RecurType { get; set; }
    }

    public class TaskRecurWeeklyLookup
    {
        public string Task { get; set; }

        public byte RecurType { get; set; }
    }
}
