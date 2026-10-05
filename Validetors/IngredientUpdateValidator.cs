using FluentValidation;
using DulcesPro.DTOs;

namespace DulcesPro.Validetors
{
    public class IngredientUpdateValidator : AbstractValidator<IngredietUpdateDTO>
    {
        public IngredientUpdateValidator()
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
