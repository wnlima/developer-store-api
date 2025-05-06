namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;

public class ListSalesCommand : ManagerListSalesCommand
{
    public Guid CustomerId { get; set; }
}