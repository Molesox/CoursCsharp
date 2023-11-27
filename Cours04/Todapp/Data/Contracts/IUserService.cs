using SharedLibrary.Repository;
using SharedLibrary.Models;

namespace DataLibrary
{
    public interface IUserService
    {
         IRepository<User> UserRepository { get; }
    }
}
