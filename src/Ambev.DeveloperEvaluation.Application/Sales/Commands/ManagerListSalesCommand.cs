using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands;

public class ManagerListSalesCommand : AbstractAdvancedFilter, IRequest<ListSalesResult>
{
}