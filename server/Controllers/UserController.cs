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
using server.Services.UserService;

namespace server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
           _userService = userService;
        }

       [HttpPost]

        [Authorize(Policy = SecurityPolicy.Customer)]
        [Authorize(Policy = SecurityPolicy.Admin)]
public async Task<IActionResult> updateUserDetailsAsync([FromBody] UserDTO userDTO)
{
    try
    {
        string userEmail = HttpContext.Items["userEmail"] as string;
        bool result = await _userService.updateUserAsync(userEmail, userDTO.isAdmin, userDTO);

        if (result)
        {
            return Ok(new { Message = "User details updated successfully." });
        }
        else
        {
            return BadRequest(new { Message = "Failed to update user details." });
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Message = "An error occurred while updating user details.", Error = ex.Message });
    }
}



    }
}
