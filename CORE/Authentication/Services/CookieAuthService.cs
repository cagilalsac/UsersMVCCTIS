using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CORE.Authentication.Services
{
   
    public class CookieAuthService : ICookieAuthService
    {
        // Field injected by the constructor to be used in
        // SignIn and SignOut methods below.
        private readonly IHttpContextAccessor _httpContextAccessor;

       
        public CookieAuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Readonly property to get the authenticated user's ID value.
        public int UserId => Convert.ToInt32(
            _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(
                claim => claim.Type == "Id").Value);

        
        public async Task SignIn(int userId, string userName,
            IEnumerable<string> userRoleNames,
            DateTime? expiration = default,
            bool isPersistent = true)
        {
            // Create claims for user ID and username,
            // then add claims for each user role.
            var claims = new List<Claim>()
            {
                new Claim("Id", userId.ToString()), 
                // custom claim with key Id and value user ID
                new Claim(ClaimTypes.Name, userName)
                // claim with key Name and value user name
            };
            foreach (var userRoleName in userRoleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRoleName));
                // claim with key Role and value user's role name
            }

            // Create a ClaimsIdentity with the generated claims
            // and specify the authentication scheme ("Cookies").
            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            // Create a ClaimsPrincipal from the identity.
            var principal = new ClaimsPrincipal(identity);

            // Set authentication properties,
            // including persistence and expiration.
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                ExpiresUtc = expiration.HasValue
                    ? DateTime.SpecifyKind(expiration.Value, DateTimeKind.Utc)
                    : null
            };

            // Sign in the user by issuing the authentication cookie.
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal,
                authenticationProperties);
        }

        
        public async Task SignOut()
        {
            // Sign out the user by removing the authentication cookie
            // from the current HTTP context.
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
