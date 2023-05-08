using AndroidKotlin.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AndroidKotlin.Auth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Test()
        {
            return Ok("Test"); 
        }

        [HttpPost]
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


        [HttpPost]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null) return BadRequest();

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null) return BadRequest();

            var userDto = new ApplicationUser { UserName = user.UserName, Email = user.Email, City = user.City };

            return Ok(userDto);
        }
    }
}
