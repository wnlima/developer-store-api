using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Eventing;

public class NotifierService : INotifierService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public NotifierService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task SaleCreated(SaleEntity sale)
    {
        var dto = _mapper.Map<SaleResult>(sale);
        await _mediator.Publish(new SaleCreatedEvent(dto));
    }

    public async Task SaleUpdated(SaleEntity sale)
    {
        var dto = _mapper.Map<SaleResult>(sale);
        await _mediator.Publish(new SaleUpdatedEvent(dto));
    }

    public async Task SaleCanceled(SaleEntity sale)
    {
        var dto = _mapper.Map<SaleResult>(sale);
        await _mediator.Publish(new SaleCanceledEvent(dto));
    }

    public async Task SaleItemCreated(SaleItemEntity saleItem)
    {
        var dto = _mapper.Map<SaleItemResult>(saleItem);
        await _mediator.Publish(new SaleItemCreatedEvent(dto));
    }

    public async Task SaleItemCanceled(SaleItemEntity saleItem)
    {
        var dto = _mapper.Map<SaleItemResult>(saleItem);
        await _mediator.Publish(new SaleItemCanceledEvent(dto));
    }
}