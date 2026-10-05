using DulcesPro.DTOs;
using FluentValidation;

namespace DulcesPro.Validetors
{
    public class IngredientInsertValidator : AbstractValidator<IngredientPostDTO>
    {
        public IngredientInsertValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Debe tener un nombre.");
            RuleFor(x => x.Unit)
                .NotEmpty()
                .Must(unit => unit == "kg" ||
                             unit == "g" ||
                             unit == "l" ||
                             unit == "ml" ||
                             unit == "unidad")
                .WithMessage("La unidad no es valida.");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a 0");

            
        }
    }
}
