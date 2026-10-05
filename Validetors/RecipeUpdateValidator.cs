using DulcesPro.DTOs;
using FluentValidation;

namespace DulcesPro.Validetors
{
    public class RecipeUpdateValidator : AbstractValidator<RecipeUpdateDTO> 
    {
        public RecipeUpdateValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Yield)
                .GreaterThan(0)
                .WithMessage("La cantidad no es valida");
        }
    }
    
}
