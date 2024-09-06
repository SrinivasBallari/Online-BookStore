using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using server.ActionFilters;
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
        [JwtEmailClaimExtractorFilter]
        [Authorize(Roles =  "customer,admin")]
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

[HttpGet]
[JwtEmailClaimExtractorFilter]
[Authorize(Roles =  "customer,admin")]
public async Task<IActionResult> GetUserDetails(){
    try
    {
        string userEmail = HttpContext.Items["userEmail"] as string;
        var result = await _userService.getUserDetails(userEmail);
        if(result != null){
            return Ok(result);
        }else{
            return BadRequest(new { Message = "Failed to fetch user details." });
        }

    }
   catch (Exception ex)
    {
        return StatusCode(500, new { Message = "An error occurred while fetching user details.", Error = ex.Message });
    }
}
       



    }
}
