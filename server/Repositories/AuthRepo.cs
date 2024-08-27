using Microsoft.EntityFrameworkCore;
using server.Models.DB;

namespace server.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly BookStoreDbContext _context;

        public AuthRepo(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<User>  CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}