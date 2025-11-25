using BookManagementAPI.Core.Models;
using System.Threading.Tasks;

namespace BookManagementAPI.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int id);
        Task AddUser(User user);
    }
}
