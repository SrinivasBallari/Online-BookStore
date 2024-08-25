using server.Models.DB;

namespace server.Repositories
{
    public interface IAuthRepo
    {
        Task<User> GetUserByEmailAsync(string Email);
        Task<User> CreateUserAsync(User user);
    }
}
