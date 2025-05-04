using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Domain.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands;

public class ListProductsCommand : AbstractAdvancedFilter, IRequest<ListProductsResult>
{
}