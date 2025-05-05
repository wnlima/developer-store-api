using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Mappers;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, SaleEntity>();
        CreateMap<SaleEntity, SaleResult>();
        CreateMap<SaleItemEntity, SaleItemResult>();
        CreateMap<CreateSaleCommand, SaleEntity>();
        CreateMap<CancelSaleCommand, SaleEntity>();
        CreateMap<SaleEntity, SaleResult>();
        CreateMap<PaginatedList<SaleEntity>, ListSalesResult>();
    }
}