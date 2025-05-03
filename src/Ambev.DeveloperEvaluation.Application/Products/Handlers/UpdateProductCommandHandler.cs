using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Ambev.DeveloperEvaluation.Application.Products.Validators;
using FluentValidation;

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
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var productEntity = await _productRepository.GetByIdAsync(request.Id);

        if (productEntity == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        _mapper.Map(request, productEntity); // Update entity properties

        await _productRepository.UpdateAsync(productEntity);

        return _mapper.Map<UpdateProductResult>(productEntity);
    }
}