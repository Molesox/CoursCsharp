using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary.Models
{
    /// <summary>
    /// Represents a to-do item with potential subtasks.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the to-do item.
        /// </summary>
        [Key]
        public int TodoItemId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user associated with the to-do item.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the parent to-do item, if any.
        /// </summary>
        public int? ParentTodoItemId { get; set; }

        /// <summary>
        /// Gets or sets the description of the to-do item.
        /// </summary>
        /// <remarks>
        /// The description is required and has a maximum length of 200 characters.
        /// </remarks>
        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the to-do item is completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the to-do item.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Gets or sets the parent to-do item, if any.
        /// </summary>
        public virtual TodoItem? ParentTodoItem { get; set; }

        /// <summary>
        /// Gets or sets the collection of subtasks associated with the to-do item.
        /// </summary>
        [ForeignKey(nameof(ParentTodoItemId))]
        public virtual ICollection<TodoItem> SubTasks { get; set; } = new List<TodoItem>();

        public override string ToString()
        {
            return ToStringIndented(0);
        }

        private string ToStringIndented(int indentationLevel)
        {
            var indent = new string('\t', indentationLevel);
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{indent}- Todoitem ID: {TodoItemId}");
            stringBuilder.AppendLine($"{indent}- Description: {Description}");
            stringBuilder.AppendLine($"{indent}- Completed: {(IsCompleted ? "Yes" : "No")}");

            if (ParentTodoItemId.HasValue)
            {
                stringBuilder.AppendLine($"{indent}- Parent todoitem ID: {ParentTodoItemId.Value}");
            }

            if (SubTasks != null && SubTasks.Any())
            {
                stringBuilder.AppendLine($"{indent}- SubTasks:");
                foreach (var subTask in SubTasks)
                {
                    stringBuilder.Append(subTask.ToStringIndented(indentationLevel + 1));
                }
            }

            return stringBuilder.ToString();
        }

    }
}
