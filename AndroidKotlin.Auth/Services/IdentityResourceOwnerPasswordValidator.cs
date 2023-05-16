using AndroidKotlin.Auth.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidKotlin.Auth.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = _userManager.FindByEmailAsync(context.UserName);

            //ErrorDto Dönülecek
            if (existUser == null)
            {
                var errors = new Dictionary<string, object>();

                errors.Add("errors", new List<string> { "Email veya şifrenizi yanlış" });
                errors.Add("status", 400);
                errors.Add("isShow", true);

                context.Result.CustomResponse = errors;
                return;
            }

            var passwordCheck = _userManager.CheckPasswordAsync(existUser.Result, context.Password);

            //ErrorDto Dönülecek
            if (!passwordCheck.Result)
            {
                var errors = new Dictionary<string, object>();

                errors.Add("errors", new List<string> { "Email veya şifrenizi yanlış" });
                errors.Add("status", 400);
                errors.Add("isShow", true);

                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(existUser.Result.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

        }
    }
}
