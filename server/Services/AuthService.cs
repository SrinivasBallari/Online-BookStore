using server.Models.DB;
using server.Repositories;
using Microsoft.AspNetCore.Identity;
using server.DTO;

namespace server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(IAuthRepo authRepo, IPasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator)
        {
            _authRepo = authRepo;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserResponseDto> LoginUserAsync(UserLoginDto loginDto)
        {
            var user = await _authRepo.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
                throw new Exception("Invalid user");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password!, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password.");

            return new UserResponseDto
            {
                Name = user.Name!,
                Email = user.Email!,
                Token = _tokenGenerator.GenerateToken(user)
            };
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRegistrationDto userDto)
        {
            var existingUser = await _authRepo.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
                throw new Exception("Username already exists.");

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email
            };

            user.Password = _passwordHasher.HashPassword(user, userDto.Password);
            await _authRepo.CreateUserAsync(user);

            return new UserResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Token = _tokenGenerator.GenerateToken(user)
            };
        }
    }
}