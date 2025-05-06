using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class GetSaleByIdRequest : ManagerGetSaleByIdRequest
{
    public Guid CustomerId { get; set; }
    public GetSaleByIdRequest(Guid id, Guid customerId) : base(id)
    {
        Id = id;
        CustomerId = customerId;
    }
}

public class ManagerGetSaleByIdRequest : IIdentifier
{
    public Guid Id { get; set; }

    public ManagerGetSaleByIdRequest(Guid id)
    {
        Id = id;
    }
}