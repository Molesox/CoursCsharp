using Microsoft.EntityFrameworkCore;
using SharedLibrary.Repository;
using System.Data;
using DataLibrary.Context;
using SharedLibrary.Models;

namespace DataLibrary.Repositories
{
    /// <summary>
    /// A repository for TodoItem entities using Entity Framework, derived from a generic EF repository base.
    /// </summary>
    public class TodoItemEFRepository : RepositoryEF<TodoItem, TodappDbContext>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TodoItemEFRepository using the specified DbContext.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public TodoItemEFRepository(TodappDbContext context) : base(context) { }

        #endregion

        #region Private Methods

        /// <summary>
        /// Deletes a TodoItem and its subtasks recursively from the database.
        /// </summary>
        /// <param name="todoItemId">The ID of the TodoItem to delete.</param>
        /// <returns>True if the delete operation was successful, false otherwise.</returns>
        private async Task<bool> DeleteTodoItemWithSubTasksAsync(int todoItemId)
        {
            var todoItem = await dbSet.AsNoTracking() // Add AsNoTracking if no update is needed
                                       .Include(t => t.SubTasks)
                                       .FirstOrDefaultAsync(t => t.TodoItemId == todoItemId);

            if (todoItem != null)
            {
                context.Entry(todoItem).State = EntityState.Deleted; // Set state to Deleted
                foreach (var subTask in todoItem.SubTasks)
                {
                    context.Entry(subTask).State = EntityState.Deleted; // Set state to Deleted for subtasks
                }
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }

        /// <summary>
        /// Recursively deletes subtasks of a TodoItem.
        /// </summary>
        /// <param name="parent">The parent TodoItem whose subtasks are to be deleted.</param>
        private void DeleteSubTasks(TodoItem parent)
        {
            foreach (var subTask in parent.SubTasks)
            {
                DeleteSubTasks(subTask); // Recursively delete subtasks
                dbSet.Remove(subTask);
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Deletes a TodoItem by its identifier, including all its subtasks.
        /// </summary>
        /// <param name="id">The identifier of the TodoItem to delete.</param>
        /// <returns>True if the delete operation was successful, false otherwise.</returns>
        public override async Task<bool> Delete(object id)
        {
            return await DeleteTodoItemWithSubTasksAsync((int)id);
        }

        #endregion

    }

}
