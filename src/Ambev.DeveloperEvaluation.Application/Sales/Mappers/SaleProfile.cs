using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Mappers;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<SaleEntity, SaleResult>()
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

        CreateMap<SaleEntity, SaleResult>();
        CreateMap<SaleItemEntity, SaleItemResult>();
        CreateMap<ManagerCancelSaleCommand, SaleEntity>();
        CreateMap<PaginatedList<SaleEntity>, ListSalesResult>();

        CreateMap<CreateSaleCommand, SaleEntity>()
           .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));
     
        CreateMap<CreateSaleItemCommand, SaleItemEntity>();
    }
}