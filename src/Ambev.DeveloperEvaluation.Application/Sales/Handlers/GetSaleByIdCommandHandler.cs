using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Salets.Validators;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class GetSaleByIdCommandHandler : IRequestHandler<GetSaleByIdCommand, SaleResult?>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdCommandHandler(ISaleRepository SaleRepository, IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _mapper = mapper;
    }

    public async Task<SaleResult> Handle(GetSaleByIdCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var SaleEntity = await _SaleRepository.GetByIdAsync(request.Id);

        if (SaleEntity == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        return _mapper.Map<SaleResult>(SaleEntity);
    }
}