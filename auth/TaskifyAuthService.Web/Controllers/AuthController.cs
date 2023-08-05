using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskify.DAL;
using Taskify.DAL.Models;
using Taskify.Utils.Services;
using TaskifyAuthService.Web.Models;
using TaskifyAuthService.Web.Services;

namespace TaskifyAuthService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly TaskifyDbContext _authDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;
        public AuthController(SignInManager<IdentityUser> signInManager,
            IJWTManagerRepository jWTManagerRepository,
            TaskifyDbContext authDb,
            UserManager<IdentityUser> userManager,
            ILoggerService loggerService)
        {
            _signInManager = signInManager;
            _jWTManager = jWTManagerRepository;
            _authDbContext = authDb;
            _logger = loggerService;
            _userManager = userManager;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Incorrect username or password!");
            }
            var token = _jWTManager.GenerateToken(model.UserName);
            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            _authDbContext.RefreshTokens.Add(new UserRefreshToken { Created = DateTime.UtcNow, RefreshToken = token.RefreshToken!, UserName = model.UserName });

            await _authDbContext.SaveChangesAsync();
            await _logger.LogInfo("Successfull authentication", model.UserName);

            return Ok(token);
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await _authDbContext.Users.AnyAsync(x => x.UserName.ToLower() == model.UserName.ToLower()))
            {
                return Ok(new { message = "This username is taken." });
            }
            var result = await _userManager.CreateAsync(new IdentityUser { UserName = model.UserName }, model.Password);

            if (result.Succeeded)
            {
                await _logger.LogInfo("Successful registration.", model.UserName);
                return Ok(new { message = "Successful registration." });
            }
            else
            {
                return Ok(new { message = result.Errors });
            }
        }
    }
}
