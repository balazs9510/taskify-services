using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskifyAuthService.Models;

namespace TaskifyAuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
