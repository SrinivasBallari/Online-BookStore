using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace server.Policies
{
    public class SecurityPolicy
    {
        public const string Admin = "admin";
        public const string Customer = "customer";

        
        public static AuthorizationPolicy AdminPolicy()
        {
            var builder = new AuthorizationPolicyBuilder();
            AuthorizationPolicy policy = builder.RequireAuthenticatedUser().RequireRole(Admin).Build();
            return policy;
        }
        public static AuthorizationPolicy CustomerPolicy()
        {
            var builder = new AuthorizationPolicyBuilder();
            AuthorizationPolicy policy = builder.RequireAuthenticatedUser().RequireRole(Customer).Build();
            return policy;
        }
    }
}