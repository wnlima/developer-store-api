namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class CancelSaleRequest
{
    public Guid Id { get; set; }

    public CancelSaleRequest(Guid id)
    {
        Id = id;
    }
}
