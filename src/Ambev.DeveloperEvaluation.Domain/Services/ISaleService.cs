using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
    Task<SaleEntity> CreateSale(SaleEntity sale, CancellationToken cancellationToken);
    Task<SaleEntity> AddSaleItemSale(SaleItemEntity saleItem, CancellationToken cancellationToken);
    Task CancelSale(Guid saleId, CancellationToken cancellationToken);
    Task<SaleEntity> CancelSaleItem(Guid saleItemId, Guid itemId, CancellationToken cancellationToken);
}