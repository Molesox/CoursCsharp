using SharedLibrary.Repository;
using SharedLibrary.Models;

namespace DataLibrary.Services
{
    /// <summary>
    /// Service class for managing User entities. Implements IUserService.
    /// </summary>
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;

        /// <summary>
        /// Gets the User repository.
        /// </summary>
        public IRepository<User> UserRepository => _userRepository;

        /// <summary>
        /// Initializes a new instance of the UserService with a User repository.
        /// </summary>
        /// <param name="userRepository">The repository for User entities.</param>
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
    }

}
