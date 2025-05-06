using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleResult>
{
    private readonly ISaleService _service;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(ISaleService SaleRepository, IMapper mapper)
    {
        _service = SaleRepository;
        _mapper = mapper;
    }

    public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var SaleEntity = _mapper.Map<SaleEntity>(command);
        SaleEntity = await _service.CreateSale(SaleEntity, cancellationToken);

        return _mapper.Map<SaleResult>(SaleEntity);
    }
}