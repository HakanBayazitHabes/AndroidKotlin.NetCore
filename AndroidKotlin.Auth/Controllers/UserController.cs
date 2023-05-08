using AndroidKotlin.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AndroidKotlin.Auth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(LocalApi.PolicyName)]
        public IActionResult Test()
        {
            return Ok("Test"); 
        }   

        public async Task<IActionResult> SignUp(SignUpViewModel signUp)
        {
            var user = new ApplicationUser
            {
                Email = signUp.Email,
                UserName = signUp.UserName,
                City = signUp.City
            };

            var result = await _userManager.CreateAsync(user, signUp.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            } 



            return NoContent();
        }
    }
}
