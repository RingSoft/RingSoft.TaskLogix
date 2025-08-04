using System.ComponentModel.DataAnnotations;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public class TlTaskHistory
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }

        public virtual TlTask Task { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }
    }
}
