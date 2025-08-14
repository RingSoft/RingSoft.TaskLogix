using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library
{
    public static class ExtensionMethods
    {
        public static int GetLastDayOfMonth(this DateTime date)
        {
            var lastDayOfMonth = new DateTime(date.Year, date.Month, 1);
            lastDayOfMonth = lastDayOfMonth.AddMonths(1)
                .AddDays(-1);

            return lastDayOfMonth.Day;
        }

        public static DateTime GetReminderDateTime(this TlTask task)
        {
            if (task.ReminderDateTime.HasValue)
            {
                if (task.SnoozeDateTime.HasValue)
                {
                    return task.SnoozeDateTime.GetValueOrDefault();
                }

                return task.ReminderDateTime.GetValueOrDefault();
            }

            return DateTime.MaxValue;
        }
    }
}
