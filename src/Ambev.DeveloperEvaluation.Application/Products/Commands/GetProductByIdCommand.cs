using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands;
public class GetProductByIdCommand : IRequest<GetProductByIdResult>
{
    public Guid Id { get; set; }

    public GetProductByIdCommand(Guid id)
    {
        Id = id;
    }
}