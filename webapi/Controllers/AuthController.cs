using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<bool> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, register.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, register.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    await _signInManager.SignInAsync(user, isPersistent: false);

                }
                else
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        [HttpPost("Login")]
        public async Task<string> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return HttpContext.User.Identity.Name;
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return "Unauthorized";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return "Unauthorized";
                }
            }
            else
            {
                return "Unauthorized";
            }
        }

        [HttpGet("isLoggedIn")]
        public bool isLoggedIn()
        {
            if (HttpContext.User.Identity.Name != null)
            {
                return true;
            }
            return false;
        }

        [HttpGet("LogOut")]
        public async Task<bool> LogOut()
        {
            
            if (HttpContext.User.Identity.Name != null)
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            return false;
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
