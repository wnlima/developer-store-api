using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Application.Products.Commands;

namespace Ambev.DeveloperEvaluation.Application.Products.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductCommand, ProductEntity>();
        CreateMap<UpdateProductCommand, ProductEntity>();
        CreateMap<ProductEntity, CreateProductResult>();
        CreateMap<ProductEntity, UpdateProductResult>();
        CreateMap<ProductEntity, GetProductByIdResult>();
        CreateMap<ProductEntity, ProductResult>();
        CreateMap<PaginatedList<ProductEntity>, ListProductsResult>();
    }
}
