using SharedLibrary.Repository;
using DataLibrary.Context;
using SharedLibrary.Models;

namespace DataLibrary.Repositories
{
    /// <summary>
    /// A repository for User entities using Entity Framework, derived from a generic EF repository base.
    /// </summary>
    public class UserEFRepository : RepositoryEF<User, TodappDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the UserEFRepository using the specified DbContext.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public UserEFRepository(TodappDbContext context) : base(context) { }
    }
}
