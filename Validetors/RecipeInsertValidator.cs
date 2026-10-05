using DulcesPro.DTOs;
using FluentValidation;
namespace DulcesPro.Validetors
{
    public class RecipeInsertValidator :AbstractValidator<RecipePostDTO>
    {
        public RecipeInsertValidator()
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
