using BookManagementAPI.Core.Models;
using System.Threading.Tasks;

namespace BookManagementAPI.Core.Interfaces
{
    public interface IAuthService
    {
        Task<(string? Token, string? Username, string? Email)> Login(string username, string password);
        Task<(string? Token, string? Username, string? Email)> Register(string username, string password, string firstname, string lastname, string email);
    }
}
