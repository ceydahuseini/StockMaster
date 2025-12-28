using System.Threading.Tasks;
using StockMaster.Models;

namespace StockMaster.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> CreateUserAsync(User user, string password);
    }
}