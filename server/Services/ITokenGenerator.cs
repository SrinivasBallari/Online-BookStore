using server.Models.DB;

namespace server.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }

}