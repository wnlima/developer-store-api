using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class GetSaleByIdRequest : IIdentifier
{
    public Guid Id { get; set; }

    public GetSaleByIdRequest(Guid id)
    {
        Id = id;
    }
}