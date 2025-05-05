using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;

public class CancelSaleCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }
}
