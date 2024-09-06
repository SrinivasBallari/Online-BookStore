using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using server.Models.DB;



namespace server.Repositories.UserRepo{
    public class UserRepo : IUserRepo
    {
         private readonly BookStoreDbContext _context;
          private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepo(BookStoreDbContext context,   IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> FetchUserByEmail(string? email)
        {
           return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            
        }

        public async Task<bool> UpdateUserDetailsAsync(User user, bool? isAdmin)
{
    var updateUser = await _context.Users
                                   .FirstOrDefaultAsync(u => u.UserId == user.UserId);

    if (updateUser == null)
    {
        return false;
    }

    if (user.Name != null)
    {
        updateUser.Name = user.Name;
    }

    if (user.Address != null)
    {
        updateUser.Address = user.Address;
    }

    if (user.Contact != null)
    {
        updateUser.Contact = user.Contact;
    }

    if (user.PinCode != null)
    {
        updateUser.PinCode = user.PinCode;
    }

    if (user.Email != null)
    {
        updateUser.Email = user.Email;
    }

    if (user.Password != null)
    {
        updateUser.Password = _passwordHasher.HashPassword(updateUser, user.Password);
    }

    if(isAdmin != null){
          updateUser.Role = "admin";
    }
    await _context.SaveChangesAsync();

    return true; 
}
    

    }

}