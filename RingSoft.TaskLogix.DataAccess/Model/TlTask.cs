using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RingSoft.TaskLogix.DataAccess.Model
{
    public class TlTask
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }
    }
}
