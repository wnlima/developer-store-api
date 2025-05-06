using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers;

public class ListSalesCommandHandler : IRequestHandler<ListSalesCommand, ListSalesResult>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMapper _mapper;

    public ListSalesCommandHandler(ISaleRepository SaleRepository, IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _mapper = mapper;
    }

    public async Task<ListSalesResult> Handle(ListSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedList =
            await _SaleRepository.ListAsync(request);

        return _mapper.Map<ListSalesResult>(paginatedList);
    }
}