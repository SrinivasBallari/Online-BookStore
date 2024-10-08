using server.Models.DB;
using server.Repositories;
using Microsoft.AspNetCore.Identity;
using server.DTO;
using server.Services.UserService;
using server.Repositories.UserRepo;

namespace server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        
      
        public UserService(IUserRepo userRepo,  IPasswordHasher<User> passwordHasher)
        {
            _userRepo = userRepo;
        

           
        }

        public async Task<bool> updateUserAsync(string email, bool? isAdmin, UserDTO? userDTO)
        {
            var userFromRepo = await _userRepo.FetchUserByEmail(email);
           
            if (userFromRepo == null)
    {
        throw new Exception($"User not found with the given email. {email}");
    }
         int userId = userFromRepo.UserId;
        
     
            var user = new User{
                UserId = userId,
                Name = userDTO.Name,
                Contact = userDTO.Contact,
                Address = userDTO.Address,
                Email = userDTO.Email,
                Password = userDTO.Password,
                PinCode = userDTO.PinCode,
            };

            return await _userRepo.UpdateUserDetailsAsync(user,isAdmin);
        }

        public async Task<UserDTO> getUserDetails(string email){
             var userFromRepo = await _userRepo.FetchUserByEmail(email);
               if (userFromRepo == null)
    {
        throw new Exception($"User not found with the given email. {email}");
    }
            var user = new UserDTO{
                Name = userFromRepo.Name,
                Address = userFromRepo.Address,
                Contact = userFromRepo.Contact,
                PinCode = userFromRepo.PinCode,
                Password = userFromRepo.Password,
                Email = userFromRepo.Email,
                isAdmin = (userFromRepo.Role == "admin")?true:false
            };
            return user;
        }
    }
}