using FluentValidation;

namespace alter.treinamento.business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(s => s.Description)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 200).WithMessage("The {PropertyName} filed must be between {MaxLength} characters");

            RuleFor(s => s.Code)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided");

            RuleFor(s => s.Reference)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided");

            RuleFor(s => s.Price)
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisionValue}");
        }
    }
}
