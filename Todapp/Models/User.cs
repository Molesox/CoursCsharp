using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Todapp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}