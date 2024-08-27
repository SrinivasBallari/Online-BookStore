namespace server.DTO
{
    public class UserRegistrationDto
    {
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string PinCode { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResponseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}