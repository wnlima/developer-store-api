using System.Net;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Functional.Auxiliary;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Newtonsoft.Json;
using Ambev.DeveloperEvaluation.TestUtils.TestData;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

namespace Ambev.DeveloperEvaluation.Functional.Sales;
public class SalesControllerTests : IAsyncLifetime, IClassFixture<HttpClientFixture>
{
    private readonly HttpClientFixture _clientFixture;

    public SalesControllerTests(HttpClientFixture clientFixture)
    {
        _clientFixture = clientFixture;
        _clientFixture.BasicDataSeed().GetAwaiter().GetResult();
        // _clientFixture.SaleSeed().GetAwaiter().GetResult();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => Task.CompletedTask;

    private async Task<PaginatedResponse<SaleResult>> DeserializePaginatedResponse(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResponse<SaleResult>>(responseContent)!;
    }

    [Fact(DisplayName = "CreateSale - Expect 201 Created on successful sale creation")]
    public async Task CreateSale_Expect_201_Created_On_Successful_Sale_Creation()
    {
        // Arrange
        var sale = SaleTestData.Generate();
        var product = _clientFixture.DbContext.Products.AsNoTracking().First();
        var saleItem = new CreateSaleItemRequest();
        saleItem.ProductId = product.Id;
        saleItem.Quantity = 10;
        sale.SaleItems = [saleItem];

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Post, "/api/sales", sale);
        var apiResponse = await _clientFixture.DeserializeApiResponseWithData<SaleResponse>(response);

        response = await _clientFixture.RequestSend(HttpMethod.Post, "/api/sales", sale);
        apiResponse = await _clientFixture.DeserializeApiResponseWithData<SaleResponse>(response);

        // Assert
        product = _clientFixture.DbContext.Products.AsNoTracking().First(x => x.Id == product.Id);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.NotEqual(Guid.Empty, apiResponse.Data.Id);
        Assert.Equal(sale.Branch, apiResponse.Data.Branch);
        Assert.Equal(product.QuantityInStock, apiResponse.Data.SaleItems.First().Product.QuantityInStock);
        Assert.Equal(_clientFixture.CustomerUser.Id, apiResponse.Data.Customer.Id);
        Assert.Equal(saleItem.ProductId, apiResponse.Data.SaleItems.First().Product.Id);
        Assert.Equal(saleItem.Quantity, apiResponse.Data.SaleItems.First().Quantity);
        Assert.Equal(saleItem.Quantity * product.Price * (1 - 0.20m), apiResponse.Data.TotalAmount);
        Assert.Equal(saleItem.Quantity, apiResponse.Data.TotalItems);
        Assert.Equal(sale.SaleDate, apiResponse.Data.SaleDate);
        Assert.Equal(sale.SaleNumber, apiResponse.Data.SaleNumber);
        Assert.Equal(saleItem.Quantity * product.Price * 0.20m, apiResponse.Data.TotalDiscounts);
        Assert.Equal(sale.IsCancelled, apiResponse.Data.IsCancelled);
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and default pagination when no parameters are provided")]
    public async Task ListSales_Expect_200_OK_And_Default_Pagination()
    {
        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, "/api/sales");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(1, apiResponse.CurrentPage);
        Assert.Equal(10, apiResponse.TotalCount);
        Assert.Equal(_clientFixture.DbContext.Sales.AsNoTracking().Count(), apiResponse.AvailableItems);
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and correct pagination when _page and _size are provided")]
    public async Task ListSales_Expect_200_OK_And_Correct_Pagination()
    {
        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, "/api/sales?_page=2&_size=5");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(2, apiResponse.CurrentPage);
        Assert.Equal(5, apiResponse.TotalCount);
        Assert.Equal(_clientFixture.DbContext.Sales.AsNoTracking().Count(), apiResponse.AvailableItems);
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and ordered results when _order is provided")]
    public async Task ListSales_Expect_200_OK_And_Ordered_Results()
    {
        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, "/api/sales?_order=price desc");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        decimal previousTotalAmount = decimal.MaxValue;
        foreach (var sale in apiResponse.Data)
        {
            Assert.True(sale.TotalAmount <= previousTotalAmount);
            previousTotalAmount = sale.TotalAmount;
        }
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and filtered results when a filter is provided")]
    public async Task ListSales_Expect_200_OK_And_Filtered_Results()
    {
        // Arrange
        var expectd = _clientFixture.DbContext.Sales.AsNoTracking().First();
        string filterValue = expectd.Branch;

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/sales?branch={filterValue}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        foreach (var sale in apiResponse.Data)
        {
            Assert.Contains(filterValue, sale.Branch);
        }
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and filtered results with partial match")]
    public async Task ListSales_Expect_200_OK_And_Filtered_Results_Partial_Match()
    {
        // Arrange
        var expectd = _clientFixture.DbContext.Sales.AsNoTracking().First();
        string partialBranch = expectd.Branch.Substring(0, 5);

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/sales?branch={partialBranch}*");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        // Assert that all sales have the partial name (adapt based on your data)
        foreach (var sale in apiResponse.Data)
        {
            Assert.StartsWith(partialBranch, sale.Branch);
        }
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and results filtered by price range")]
    public async Task ListSales_Expect_200_OK_And_Results_Filtered_By_TotalAmount_Range()
    {
        // Arrange
        decimal minTotalAmount = 10;
        decimal maxTotalAmount = 20;

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/sales?_minTotalAmount={minTotalAmount}&_maxTotalAmount={maxTotalAmount}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        foreach (var sale in apiResponse.Data)
        {
            Assert.True(sale.TotalAmount >= minTotalAmount && sale.TotalAmount <= maxTotalAmount);
        }
    }

    [Fact(DisplayName = "ListSales - Expect 400 Bad Request when _page or _size is invalid")]
    public async Task ListSales_Expect_400_Bad_Request_When_Page_Or_Size_Is_Invalid()
    {
        // Act
        var response1 = await _clientFixture.RequestSend(HttpMethod.Get, "/api/sales?_page=0");
        var response2 = await _clientFixture.RequestSend(HttpMethod.Get, "/api/sales?_size=0");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response1.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and empty list when no results match the filter")]
    public async Task ListSales_Expect_200_OK_And_Empty_List_When_No_Results_Match_Filter()
    {
        // Arrange
        string nonExistentBranch = "NonExistentSale";

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/sales?branch={nonExistentBranch}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.Empty(apiResponse.Data);
    }

    [Fact(DisplayName = "ListSales - Expect 200 OK and results ordered by multiple fields")]
    public async Task ListSales_Expect_200_OK_And_Results_Ordered_By_Multiple_Fields()
    {
        // Arrange
        var firstExpectd = _clientFixture.DbContext.Sales.AsNoTracking().OrderByDescending(p => p.TotalAmount).ThenBy(p => p.Branch).First();
        var lastExpectd = _clientFixture.DbContext.Sales.AsNoTracking().OrderByDescending(p => p.TotalAmount).ThenBy(p => p.Branch).Take(10).Last();

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, "/api/sales?_order=price desc,name asc");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await DeserializePaginatedResponse(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);

        Assert.Equal(firstExpectd.Branch, apiResponse.Data.First().Branch);
        Assert.Equal(firstExpectd.TotalAmount, apiResponse.Data.First().TotalAmount);
        Assert.Equal(lastExpectd.Branch, apiResponse.Data.Last().Branch);
        Assert.Equal(lastExpectd.TotalAmount, apiResponse.Data.Last().TotalAmount);
    }

    [Fact(DisplayName = "CreateSale - Expect 400 Bad Request when sale name is missing")]
    public async Task CreateSale_Expect_400_Bad_Request_When_Branch_Is_Missing()
    {
        // Arrange
        var sale = SaleTestData.Generate();
        sale.Branch = "";

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Post, "/api/sales", sale);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(DisplayName = "GetSaleById - Expect 200 OK on successful sale retrieval")]
    public async Task GetSaleById_Expect_200_OK_On_Successful_Sale_Retrieval()
    {
        // Arrange
        var sale = _clientFixture.DbContext.Sales.AsNoTracking().First();

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/sales/{sale.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await _clientFixture.DeserializeApiResponseWithData<SaleResult>(response);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(sale.Id, apiResponse.Data.Id);
        Assert.Equal(sale.Branch, apiResponse.Data.Branch);
    }

    [Fact(DisplayName = "GetSaleById - Expect 404 Not Found when sale ID does not exist")]
    public async Task GetSaleById_Expect_404_Not_Found_When_Sale_ID_Does_Not_Exist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/sales/{nonExistentId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact(DisplayName = "CancelSale - Expect 204 NoContent on successful sale ")]
    public async Task CancelSale_Expect_204_NoContent_On_Successful_Sale_Deletion()
    {
        // Arrange
        var sale = SaleTestData.Generate();
        var createResponse = await _clientFixture.RequestSend(HttpMethod.Post, "/api/sales", sale);
        createResponse.EnsureSuccessStatusCode();
        var createdSale = await _clientFixture.DeserializeApiResponseWithData<SaleResult>(createResponse);

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Delete, $"/api/sales/{createdSale.Data.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact(DisplayName = "CancelSale - Expect 404 Not Found when sale ID does not exist")]
    public async Task CancelSale_Expect_404_Not_Found_When_Sale_ID_Does_Not_Exist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Delete, $"/api/sales/{nonExistentId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}