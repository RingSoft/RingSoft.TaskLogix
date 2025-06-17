using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public enum DailyRecurTypes
    {
        [Description("Every X Days")]
        EveryXDays = 0,
        [Description("Every Weekday")]
        EveryWeekday = 1,
        [Description("Regenerate X Days After Completed")]
        RegenerateXDaysAfterCompleted = 2,
    }

public class TlTaskRecurDaily
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskId { get; set; }

        public virtual TlTask Task { get; set; }

        [Required]
        [DefaultValue(0)]
        public byte RecurType { get; set; }

        public int? RecurDays { get; set; }

        public int? RegenDaysAfterCompleted { get; set; }
    }
}
