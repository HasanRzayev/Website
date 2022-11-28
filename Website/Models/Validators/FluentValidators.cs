using FluentValidation;
using Website.Models.Entity;
using Website.Models.ViewModels;

namespace Website.Models.Validators
{
    public class FluentValidators: AbstractValidator<Catalogue>
    {
        public FluentValidators()
        {
            RuleFor(catalog => catalog.Name).NotNull().WithMessage("Bos daxil etmek olmaz!!!!");
        }
       
    }
}
