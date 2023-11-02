using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todapp.Models
{
    public class TodoItem
    {
        [Key]
        public int TodoItemId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        // Foreign Key for User
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation Property for User
        public virtual User User { get; set; }

        // Self-referencing relationship to support subtasks
        public int? ParentTodoItemId { get; set; }
        public virtual TodoItem ParentTodoItem { get; set; } 

        // Navigation Property for subtasks
        public virtual ICollection<TodoItem> SubTasks { get; set; } = new List<TodoItem>();
    }
}