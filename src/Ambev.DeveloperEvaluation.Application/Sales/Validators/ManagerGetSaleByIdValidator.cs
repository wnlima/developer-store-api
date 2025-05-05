using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Salets.Validators;

public class ManagerGetSaleByIdValidator : AbstractValidator<ManagerGetSaleByIdCommand>
{
    public ManagerGetSaleByIdValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}