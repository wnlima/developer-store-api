using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.Commands;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate request (using FluentValidation)

        var productEntity = await _productRepository.GetByIdAsync(request.Id);

        if (productEntity == null)
        {
            // Handle not found
            return null;
        }

        _mapper.Map(request, productEntity); // Update entity properties

        await _productRepository.UpdateAsync(productEntity);

        return _mapper.Map<UpdateProductResult>(productEntity);
    }
}