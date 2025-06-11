using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public enum TaskStatusTypes
    {
        [Description("Not Started")]
        NotStarted = 0,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Completed")]
        Completed = 3,
        [Description("Waiting on Someone Else")]
        WaitingOnSomeoneElse = 4,
        [Description("Deferred")]
        Deferred = 5,
    }

    public enum TaskPriorityTypes
    {
        Low = 0,
        Normal = 1,
        High = 3,
    }
    public class TlTask
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [Required]
        public DateTime StartDate { get; set; } 

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReminderDateTime { get; set; }

        [Required]
        [DefaultValue(0)]
        public byte StatusType { get; set; }

        [Required]
        [DefaultValue(1)]
        public byte PriorityType { get; set; }

        [Required]
        [DefaultValue(0)]
        public double PercentComplete { get; set; }
    }
}
