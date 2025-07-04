using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;

public class ManagerGetSaleByIdCommand : IRequest<SaleResult>
{
    public Guid Id { get; set; }
}