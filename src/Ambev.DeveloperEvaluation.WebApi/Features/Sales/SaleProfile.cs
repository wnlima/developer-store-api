using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class SaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public SaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<SaleResult, SaleResponse>();
        CreateMap<SaleItemRequest, SaleItemCommand>();

        CreateMap<CancelSaleRequest, CancelSaleCommand>();

        CreateMap<ListSalesRequest, ListSalesCommand>();
    }
}