
using DulcesPro.DTOs;
using FluentValidation;
namespace DulcesPro.Validetors
{
    public class RecipeIngredientUpdateValidator : AbstractValidator<RecipeIngredientUpdateDTO>
    {
        public RecipeIngredientUpdateValidator()
        {
            RuleFor(x=>x.Quantity)
                .GreaterThanOrEqualTo(1).WithMessage("La cantidad debe ser mayor a 0");

        }
    }
}
