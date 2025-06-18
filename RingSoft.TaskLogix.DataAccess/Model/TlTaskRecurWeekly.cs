using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public enum WeeklyRecurTypes
    {
        [Description("Every X Weeks On")]
        EveryXWeeks = 0,
        [Description("Regenerate X Weeks After Completed")]
        RegenerateXWeeksAfterCompleted = 1,
    }

    public class TlTaskRecurWeekly
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskId { get; set; }

        public virtual TlTask Task { get; set; }

        [Required]
        [DefaultValue(0)]
        public byte RecurType { get; set; }

        public int? RecurWeeks { get; set; }

        public bool? Sunday { get; set; }

        public bool? Monday { get; set; }

        public bool? Tuesday { get; set; }

        public bool? Wednesday { get; set; }

        public bool? Thursday { get; set; }

        public bool? Friday { get; set; }

        public bool? Saturday { get; set; }

        public int? RegenWeeksAfterCompleted { get; set; }
    }
}
