using System.Threading.Tasks;
using server.Models.DB;



namespace server.Repositories.UserRepo{
    public interface IUserRepo
{
   Task<bool> UpdateUserDetailsAsync(User user, bool? isAdmin);
   Task<User> FetchUserByEmail(string? email);
}

}