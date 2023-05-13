using Microsoft.AspNetCore.Identity;

namespace AndroidKotlin.Auth.Services
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"Bu '{email}' adresi kullanılmaktadır."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"Bu '{userName}' kullanıcı adı kullanılmaktadır."
            };
        }
    }
}
