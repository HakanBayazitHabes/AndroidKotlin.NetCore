using AndroidKotlin.Auth.Models;
using FluentValidation;

namespace AndroidKotlin.Auth.Validation
{
    public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpViewModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı gereklidir.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email gereklidir.").EmailAddress().WithMessage("Email adresi doğru formatta değil");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre gereklidir.");
            RuleFor(x=>x.City).NotEmpty().WithMessage("Şehir gereklidir.");
        }
    }
}
