using AndroidKotlin.API.Models;
using FluentValidation;

namespace AndroidKotlin.API.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün ismi gereklidir.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün fiyatı gereklidir.");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Ürün stok adedi gereklidir.");
            RuleFor(x => x.Color).NotEmpty().WithMessage("Ürün rengi gereklidir.");

        }
    }
}
