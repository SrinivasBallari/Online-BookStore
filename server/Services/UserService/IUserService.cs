using server.DTO;
using server.Models.DB;

namespace server.Services.UserService
{
    public interface IUserService
    {
        Task<bool> updateUserAsync(string email, bool? isAdmin,UserDTO? userDTO);
          public Task<UserDTO> getUserDetails(string email);
    }

}