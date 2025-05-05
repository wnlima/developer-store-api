using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators;

public class GetSaleByIdRequestValidator : GetIdRequestValidator<GetSaleByIdRequest>
{
    protected override string Message => "Product ID is required";
}
