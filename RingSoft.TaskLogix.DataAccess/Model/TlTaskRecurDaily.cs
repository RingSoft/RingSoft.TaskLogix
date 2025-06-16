using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public class TlTaskRecurDaily
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskId { get; set; }
    }
}
