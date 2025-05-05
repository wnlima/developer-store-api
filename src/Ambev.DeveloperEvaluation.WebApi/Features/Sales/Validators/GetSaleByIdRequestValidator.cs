using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators;

public class GetSaleByIdRequestValidator : GetIdRequestValidator<GetSaleByIdRequest>
{
    protected override string Message => "Product ID is required";
}

public class ManagerGetSaleByIdRequestValidator : GetIdRequestValidator<ManagerGetSaleByIdRequest>
{
    protected override string Message => "Product ID is required";
    public ManagerGetSaleByIdRequestValidator() : base()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
