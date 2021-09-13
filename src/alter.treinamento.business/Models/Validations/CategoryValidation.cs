using FluentValidation;

namespace alter.treinamento.business.Models.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(s => s.Description)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 200).WithMessage("The {PropertyName} filed must be between {MaxLength} characters");
        }
    }
}
