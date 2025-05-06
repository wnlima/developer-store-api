using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validators;

public class SaleItemEntityValidator : AbstractValidator<SaleItemEntity>
{
    public SaleItemEntityValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("Quantity cannot be greater than 20.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
    }
}