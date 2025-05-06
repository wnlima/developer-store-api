namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class ManagerCancelSaleRequest
{
    public Guid Id { get; set; }

    public ManagerCancelSaleRequest(Guid id)
    {
        Id = id;
    }
}
