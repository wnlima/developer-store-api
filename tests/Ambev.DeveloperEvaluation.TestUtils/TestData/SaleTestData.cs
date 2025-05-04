using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestUtils.TestData;

/// <summary>
/// Provides methods for generating realistic sale data using the Bogus library.
/// This class ensures consistency in test data creation and offers flexibility
/// for generating various sale scenarios.
/// </summary>
public static class SaleTestData
{
    /// <summary>
    /// Faker instance for generating CreateSaleCommand objects with randomized and realistic data.
    /// </summary>
    private static readonly Faker<SaleEntity> _faker = new Faker<SaleEntity>()
        .RuleFor(s => s.SaleNumber, f => f.Commerce.Ean13())
        .RuleFor(s => s.SaleDate, f => f.Date.Past(1)) // dentro do último ano
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(10, 1000))
        .RuleFor(s => s.TotalItems, f => f.Random.Decimal(1, 100))
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(s => s.IsCancelled, f => false)
        .RuleFor(s => s.Customer, f => UserTestData.GenerateValidUser());

    /// <summary>
    /// Generates a list of Sale objects.
    /// </summary>
    /// <param name="count">The number of sales to generate.</param>
    /// <returns>A list of Sale objects.</returns>
    public static List<SaleEntity> Generate(int count)
    {
        return _faker.Generate(count);
    }

    /// <summary>
    /// Generates a single valid CreateSaleCommand object.
    /// </summary>
    /// <returns>A CreateSaleCommand object with valid data.</returns>
    public static SaleEntity Generate()
    {
        return _faker.Generate();
    }
}