namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;
public class GetSaleByIdCommand : ManagerGetSaleByIdCommand
{
    public Guid CustomerId { get; set; }
}