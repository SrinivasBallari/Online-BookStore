using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.DTO;
using server.Models.DB;
using server.Policies;
using server.Services;

namespace server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register's a new user
        /// </summary>
        /// <returns>a jwt-token that is to be stored on the client .</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userDto)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(userDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        /// <summary>
        /// Verify user login details and provide a jwt token upon successfull authentication.
        /// </summary>
        /// <returns>a jwt-token that is to be stored on the client.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginUserAsync(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpGet("verifyUserRole")]
        [Authorize(Policy = SecurityPolicy.Admin)]
        public IActionResult VerifyUserRole(){
            VerifyUserRoleDTO obj = new VerifyUserRoleDTO {
                message = "true"
            };
            return Ok(obj);
        }
    }
}
