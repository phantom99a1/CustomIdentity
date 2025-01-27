using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    }
}
