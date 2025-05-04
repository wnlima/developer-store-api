using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface INotifierService
{
    Task SaleCanceled(SaleEntity sale);
    Task SaleCreated(SaleEntity sale);
    Task SaleUpdated(SaleEntity sale);
    Task SaleItemCreated(SaleItemEntity saleItem);
    Task SaleItemCanceled(SaleItemEntity saleItem);
}
