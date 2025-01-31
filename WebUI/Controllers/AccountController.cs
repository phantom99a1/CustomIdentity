using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUI.Entities;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : ControllerBase
    {        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(ApplicationUser user)
        {
            try
            {
                var now = DateTime.Now;
                var newUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    CreateDateTime = now,
                    LastModifiedDateTime = now,
                    LastLogin = now,
                };
                var result = await userManager.CreateAsync(newUser);
                return !result.Succeeded ? BadRequest(result) : Ok(new {result, message = "Registered Successfully!"});
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong, please try again. {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var now = DateTime.Now;
                var user = await userManager.FindByEmailAsync(login.Email);
                if(user != null && !user!.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                }
                var result = await signInManager.PasswordSignInAsync(user!, login.Password, login.RememberMe, false);
                
                if(!result.Succeeded)
                {
                    return Unauthorized("Check your credentials and try again");
                }
                user!.LastModifiedDateTime = now;
                user!.LastLogin = now;
                await userManager.UpdateAsync(user);
                return Ok(new { result, message = "Login Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong, please try again. {ex.Message}");
            }
        }

        [HttpGet("Logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok(new { message = "You are free to go!" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Some thing went wrong, please try again. {ex.Message}");
            }
        }

        [HttpGet("Admin"), Authorize]
        public IActionResult AdminPage()
        {
            string[] partners = ["TienDuat", "MyLinh", "Luffy", "Naruto", "Hinata", "Harry", "Ginny", "Zoro"];
            return Ok(new { trustedPartners = partners });
        }

        [HttpGet("Home/{email}"), Authorize]
        public async Task<IActionResult> HomePage(string email)
        {
            var userInfo = await userManager.FindByEmailAsync(email);
            return userInfo == null ? NotFound() : Ok(userInfo);
        }

        [HttpGet("CheckUser"), Authorize]
        public async Task<IActionResult> CheckUser()
        {
            try
            {
                var user = HttpContext.User;
                var principals = new ClaimsPrincipal(user);
                var result = signInManager.IsSignedIn(principals);
                if (result)
                {
                    var currentUser = await signInManager.UserManager.GetUserAsync(principals);
                    return Ok(new { message = "Logged in", currentUser });
                }
                else
                {
                    return Forbid("Access Denied");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Some thing went wrong, please try again {ex.Message}");
            }
        }
    }
}
