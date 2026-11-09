using APP.Authentication.Models;
using CORE.Models;

namespace APP.Authentication.Services
{
    public interface IAuthService
    {
        public Task<CommandResponse> Login(LoginRequest request);

        public Task Logout();
    }
}
