using System.Net;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Functional.Auxiliary;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Newtonsoft.Json;
using Ambev.DeveloperEvaluation.TestUtils.TestData;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.TestUtils;
using Ambev.DeveloperEvaluation.Application.Products.Commands;

namespace Ambev.DeveloperEvaluation.Functional.Products;
public class ListProductsControllerTests : IAsyncLifetime, IClassFixture<HttpClientFixture>
{
    private readonly HttpClientFixture _clientFixture;
    private static List<CreateProductCommand> _products = new List<CreateProductCommand>();

    public ListProductsControllerTests(HttpClientFixture clientFixture)
    {
        _clientFixture = clientFixture;
        if (_products.Count == 0)
        {
            _products = ProductTestData.GenerateProducts(25);
            SeedDatabase().GetAwaiter().GetResult();
        }
    }

    private async Task SeedDatabase()
    {
        foreach (var product in _products)
        {
            var response = await _clientFixture.Client.PostAsJsonAsync("/api/products", product);
            response.EnsureSuccessStatusCode();
        }
    }

    public Task InitializeAsync()
           => Task.CompletedTask;

    public async Task DisposeAsync()
    {

    }

    private async Task<PaginatedResponse<ProductResult>> DeserializeApiResponse(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var ret = JsonConvert.DeserializeObject<JsonResponseWrapper<PaginatedResponse<ProductResult>>>(responseContent);
        return ret!.Data;
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and default pagination when no parameters are provided")]
    public async Task ListProducts_Expect_200_OK_And_Default_Pagination()
    {
        // Act
        var response = await _clientFixture.Client.GetAsync("/api/products");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(1, apiResponse.CurrentPage);
        Assert.Equal(10, apiResponse.TotalCount);
        Assert.Equal(25, apiResponse.AvailableItems);
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and correct pagination when _page and _size are provided")]
    public async Task ListProducts_Expect_200_OK_And_Correct_Pagination()
    {
        // Act
        var response = await _clientFixture.Client.GetAsync("/api/products?_page=2&_size=5");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(2, apiResponse.CurrentPage);
        Assert.Equal(5, apiResponse.TotalCount);
        Assert.Equal(25, apiResponse.AvailableItems);
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and ordered results when _order is provided")]
    public async Task ListProducts_Expect_200_OK_And_Ordered_Results()
    {
        // Act
        var response = await _clientFixture.Client.GetAsync("/api/products?_order=price desc");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        decimal previousPrice = decimal.MaxValue;
        foreach (var product in apiResponse.Data)
        {
            Assert.True(product.Price <= previousPrice);
            previousPrice = product.Price;
        }
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and filtered results when a filter is provided")]
    public async Task ListProducts_Expect_200_OK_And_Filtered_Results()
    {
        // Arrange
        var expectd = _products.First();
        string filterValue = expectd.Name;

        // Act
        var response = await _clientFixture.Client.GetAsync($"/api/products?name={filterValue}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        foreach (var product in apiResponse.Data)
        {
            Assert.Contains(filterValue, product.Name);
        }
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and filtered results with partial match")]
    public async Task ListProducts_Expect_200_OK_And_Filtered_Results_Partial_Match()
    {
        // Arrange
        var expectd = _products.First();
        string partialName = expectd.Name.Substring(0, 5);

        // Act
        var response = await _clientFixture.Client.GetAsync($"/api/products?name={partialName}*");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        // Assert that all products have the partial name (adapt based on your data)
        foreach (var product in apiResponse.Data)
        {
            Assert.StartsWith(partialName, product.Name);
        }
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and results filtered by price range")]
    public async Task ListProducts_Expect_200_OK_And_Results_Filtered_By_Price_Range()
    {
        // Arrange
        decimal minPrice = 10;
        decimal maxPrice = 20;

        // Act
        var response = await _clientFixture.Client.GetAsync($"/api/products?_minPrice={minPrice}&_maxPrice={maxPrice}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        foreach (var product in apiResponse.Data)
        {
            Assert.True(product.Price >= minPrice && product.Price <= maxPrice);
        }
    }

    [Fact(DisplayName = "ListProducts - Expect 400 Bad Request when _page or _size is invalid")]
    public async Task ListProducts_Expect_400_Bad_Request_When_Page_Or_Size_Is_Invalid()
    {
        // Act
        var response1 = await _clientFixture.Client.GetAsync("/api/products?_page=0");
        var response2 = await _clientFixture.Client.GetAsync("/api/products?_size=0");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response1.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and empty list when no results match the filter")]
    public async Task ListProducts_Expect_200_OK_And_Empty_List_When_No_Results_Match_Filter()
    {
        // Arrange
        string nonExistentName = "NonExistentProduct";

        // Act
        var response = await _clientFixture.Client.GetAsync($"/api/products?name={nonExistentName}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.Empty(apiResponse.Data);
    }

    [Fact(DisplayName = "ListProducts - Expect 200 OK and results ordered by multiple fields")]
    public async Task ListProducts_Expect_200_OK_And_Results_Ordered_By_Multiple_Fields()
    {
        // Arrange
        var firstExpectd = _products.OrderByDescending(p => p.Price).ThenBy(p => p.Name).First();
        var lastExpectd = _products.OrderByDescending(p => p.Price).ThenBy(p => p.Name).Take(10).Last();

        // Act
        var response = await _clientFixture.Client.GetAsync("/api/products?_order=price desc,name asc");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializeApiResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        Assert.Equal(firstExpectd.Name, apiResponse.Data.First().Name);
        Assert.Equal(firstExpectd.Price, apiResponse.Data.First().Price);
        Assert.Equal(lastExpectd.Name, apiResponse.Data.Last().Name);
        Assert.Equal(lastExpectd.Price, apiResponse.Data.Last().Price);
    }
}