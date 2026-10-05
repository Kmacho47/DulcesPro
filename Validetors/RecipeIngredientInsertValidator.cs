using DulcesPro.DTOs;
using FluentValidation;
namespace DulcesPro.Validetors
{
    public class RecipeIngredientInsertValidator : AbstractValidator<RecipeIngredientPostDTO>
    {
        public RecipeIngredientInsertValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0");
            RuleFor(x => x.IngredientId)
                .NotEmpty()
                .WithMessage("Debe tener un ingredienteID");
            RuleFor(x => x.RecipeId)
               .NotEmpty()
               .WithMessage("Debe tener un RecipeID");

        }
    }
}
