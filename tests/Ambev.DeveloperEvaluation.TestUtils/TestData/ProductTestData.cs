using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Bogus;

namespace Ambev.DeveloperEvaluation.TestUtils.TestData;

/// <summary>
/// Provides methods for generating realistic product data using the Bogus library.
/// This class ensures consistency in test data creation and offers flexibility
/// for generating various product scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Faker instance for generating CreateProductCommand objects with randomized and realistic data.
    /// </summary>
    private static readonly Faker<CreateProductCommand> _createProductFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.QuantityInStock, f => f.Random.Int(0, 100));

    /// <summary>
    /// Generates a list of CreateProductCommand objects.
    /// </summary>
    /// <param name="count">The number of products to generate.</param>
    /// <returns>A list of CreateProductCommand objects.</returns>
    public static List<CreateProductCommand> GenerateProducts(int count)
    {
        return _createProductFaker.Generate(count);
    }

    /// <summary>
    /// Generates a single valid CreateProductCommand object.
    /// </summary>
    /// <returns>A CreateProductCommand object with valid data.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return _createProductFaker.Generate();
    }
}