using APP.Authentication.Models;
using APP.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    // ~/Auth/Login
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("~/[action]")] // ~/Login
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("~/[action]")] // ~/Login
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.Login(request);
                if (response.IsSuccessful)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("", response.Message);
            }
            return View();
        }

        [Route("~/[action]")] // ~/Logout
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Login", "Auth");
        }
    }
}
