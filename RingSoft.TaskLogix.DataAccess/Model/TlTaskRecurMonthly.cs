using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public enum MonthlyRecurTypes
    {
        [Description("Day X Of Every Y Months")]
        DayXOfEveryYMonths = 0,
        [Description("Xth Weekday Of Every Y Months")]
        XthWeekdayOfEveryYMonths = 1,
        [Description("Regenerate X Months After Completed")]
        RegenerateXMonthsAfterCompleted = 2,
    }

    public enum WeekTypes
    {
        First = 0,
        Second = 1,
        Third = 2,
        Fourth = 3,
        Last = 4,
    }

    public enum DayTypes
    {
        [Description("Weekday")]
        Weekday = 0,
        [Description("Weekend Day")]
        WeekendDay = 1,
        [Description("Sunday")]
        Sunday = 2,
        [Description("Monday")]
        Monday = 3,
        [Description("Tuesday")]
        Tuesday = 4,
        [Description("Wednesday")]
        Wednesday = 5,
        [Description("Thursday")]
        Thursday = 6,
        [Description("Friday")]
        Friday = 7,
        [Description("Saturday")]
        Saturday = 8,
    }
    public class TlTaskRecurMonthly
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskId { get; set; }

        public virtual TlTask Task { get; set; }

        [Required]
        [DefaultValue(0)]
        public byte RecurType { get; set; }

        public int? DayXOfEvery { get; set; }

        public int? OfEveryYMonths { get; set; }

        public byte? WeekType { get; set; }

        public byte? DayType { get; set; }

        public int? OfEveryWeekTypeMonths { get; set; }

        public int? RegenMonthsAfterCompleted { get; set; }
    }
}
