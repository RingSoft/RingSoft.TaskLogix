using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public enum YearlylyRecurTypes
    {
        [Description("Every Month Day X")]
        EveryMonthDayX = 0,
        [Description("The Nth Weekday Type Of Month")]
        TheNthWeekdayTypeOfMonth = 1,
        [Description("Regenerate X Years After Completed")]
        RegenerateXYearsAfterCompleted = 2,
    }

    public enum MonthsInYear
    {
        January = 0,
        February = 1,
        March = 2,
        April = 3,
        May = 4,
        June = 5,
        July = 6,
        August = 7,
        September = 8,
        October = 9,
        November = 10,
        December = 11,
    }

    public class TlTaskRecurYearly
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskId { get; set; }

        public virtual TlTask Task { get; set; }

        [Required]
        [DefaultValue(0)]
        public byte RecurType { get; set; }

        public byte? EveryMonthType { get; set; }

        public int? MonthDay { get; set; }

        public byte? WeekType { get; set; }

        public byte? DayType { get; set; }

        public byte? WeekMonthType { get; set; }

        public int? RegenYearsAfterCompleted { get; set; }
    }
}
