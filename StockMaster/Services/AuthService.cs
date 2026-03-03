using System.Threading.Tasks;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace StockMaster.Services
{
    public class AuthService : IAuthService
    {
        private readonly StockDbContext _context;

        public AuthService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

            if (user == null)
                return null;

            bool isHashed = user.Password.StartsWith("$2") && user.Password.Length == 60;

            if (isHashed)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    return user;
            }
            else
            {
                if (user.Password == password)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                    await _context.SaveChangesAsync();

                    return user;
                }
            }

            return null;
        }        
        

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        
    }
}