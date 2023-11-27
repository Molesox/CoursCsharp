namespace Todapp
{
    /// <summary>
    /// Represents a command interface with an asynchronous execution method.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Asynchronously executes the command.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task Execute();
    }
}