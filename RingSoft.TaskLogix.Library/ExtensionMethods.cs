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
    }
}
