using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class SaleService : ISaleService
{
    private readonly IDiscountService _discountService;
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly INotifierService _notifier;

    public SaleService(IDiscountService discountService, ISaleRepository saleRepository, IProductRepository productRepository, INotifierService notifier)
    {
        _discountService = discountService;
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _notifier = notifier;
    }

    public async Task<SaleEntity> CreateSale(SaleEntity sale, CancellationToken cancellationToken)
    {
        if (sale.SaleItems == null || !sale.SaleItems.Any())
            throw new ValidationException("Sale must have at least one item");

        var ids = sale.SaleItems.Select(o => o.ProductId).ToArray();
        var products = await _productRepository.GetProducts(ids);
        var invalidProducts = sale.SaleItems.Where(x => !products.Any(p => p.Id == x.ProductId)).Select(x => x.ProductId).ToArray();

        if (invalidProducts.Any())
            throw new ValidationException($"Product ID is invalid {string.Join(", ", invalidProducts)}");

        foreach (var item in sale.SaleItems)
        {
            var p = products.First(p => p.Id == item.ProductId);
            item.UnitPrice = p.Price;
            await _discountService.Apply(item);
            p.QuantityInStock -= item.Quantity;
        }

        sale.Compute();

        sale.Customer = null;
        await _saleRepository.CreateAsync(sale, cancellationToken);

        sale = (await _saleRepository.GetDetailsAsync(sale.Id, cancellationToken))!;
        await _notifier.SaleCreated(sale);

        return sale;
    }

    public async Task<SaleEntity> AddSaleItemSale(SaleItemEntity saleItem, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetDetailsAsync(saleItem.SaleId, cancellationToken, true);

        if (sale == null)
            throw new ValidationException($"Sale with ID {saleItem.SaleId} is invalid");

        var product = await _productRepository.GetByIdAsync(saleItem.ProductId, cancellationToken);

        if (product == null)
            throw new ValidationException($"Product with ID {saleItem.SaleId} is invalid");

        if (sale.SaleItems == null || !sale.SaleItems.Any())
            throw new ValidationException("Sale must have at least one item");

        saleItem.Product = product;

        await _discountService.Apply(saleItem);
        sale.SaleItems.Add(saleItem);
        sale.Compute();
        saleItem.Product = null;

        await _saleRepository.UpdateAsync(sale);
        await _notifier.SaleItemCreated(saleItem);
        await _notifier.SaleUpdated(sale);

        return sale;
    }

    public async Task CancelSale(Guid saleId, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetDetailsAsync(saleId, cancellationToken, true);

        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {saleId} not found");

        sale.IsCancelled = true;
        await _saleRepository.UpdateAsync(sale);

        if (sale != null)
            await _notifier.SaleCanceled(sale);
    }

    public async Task<SaleEntity> CancelSaleItem(Guid saleItemId, Guid itemId, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetDetailsAsync(saleItemId, cancellationToken, true);

        if (sale == null)
            throw new ValidationException($"Sale with ID {saleItemId} is invalid");

        var saleItem = sale.SaleItems.FirstOrDefault(x => x.Id == saleItemId);

        if (saleItem == null)
            throw new KeyNotFoundException($"Sale item wity ID {saleItemId} not found");

        saleItem.IsCancelled = true;

        await _discountService.Apply(saleItem);
        sale.SaleItems.Add(saleItem);
        sale.Compute();

        await _saleRepository.UpdateAsync(sale);
        await _notifier.SaleItemCanceled(saleItem);
        await _notifier.SaleUpdated(sale);

        return sale;
    }
}