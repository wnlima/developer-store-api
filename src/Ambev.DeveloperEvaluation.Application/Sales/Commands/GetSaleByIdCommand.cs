using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;
public class GetSaleByIdCommand : IRequest<SaleResult>
{
    public Guid Id { get; set; }

    public GetSaleByIdCommand(Guid id)
    {
        Id = id;
    }
}