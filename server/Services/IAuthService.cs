using server.DTO;
using server.Models.DB;

namespace server.Services
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegisterUserAsync(UserRegistrationDto userDto);
        Task<UserResponseDto> LoginUserAsync(UserLoginDto loginDto);
    }

}