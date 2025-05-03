using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;
/// <summary>
/// Profile for mapping between Application and API CreateProduct responses
/// </summary>
public class ProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct feature
    /// </summary>
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();

        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<DeleteProductRequest, DeleteProductCommand>();

        CreateMap<ListProductsRequest, ListProductsCommand>();
    }
}