using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using server.Models.DB;
using System.Security.Claims;

namespace server.ActionFilters
{
    public class JwtEmailClaimExtractorFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken != null)
                {
                    var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                    if (emailClaim != null)
                    {
                        // Set the user identity and continue the request
                        var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                        context.HttpContext.Items["userEmail"] = emailClaim;
                        base.OnActionExecuting(context);
                        return;
                    }
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }

}
