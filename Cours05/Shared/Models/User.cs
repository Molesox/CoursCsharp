using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Models
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <remarks>
        /// The user's name is required and has a maximum length of 50 characters.
        /// </remarks>
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of to-do items associated with the user.
        /// </summary>
        [ForeignKey(nameof(TodoItem.UserId))]
        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
