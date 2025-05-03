using System.Net.Http.Json;
using System.Text;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.TestUtils.TestData;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Ambev.DeveloperEvaluation.Functional.Auxiliary;

public sealed class HttpClientFixture : IDisposable
{
    internal const string COLLECTION_NAME = "HttpClient collection";
    private readonly CustomWebApplicationFactory<Program> _factory;
    private HttpClient? _client;
    private bool ProductsLoad;

    public HttpClientFixture()
    {
        _factory = new CustomWebApplicationFactory<Program>();
    }

    public HttpClient Client
    {
        get
        {
            _client ??= _factory.CreateClient();
            return _client;
        }
    }

    public IServiceProvider Services
        => _factory.Services;

    public IConfigurationRoot Configuration
        => _factory.Configuration;

    public static StringContent CreateContent<T>(T obj) where T : class
        => new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

    public void SetDIBehaviors(params Action<IServiceCollection>[] behaviors)
    {
        _factory.SetDIBehaviors(behaviors);
    }

    public async Task<ApiResponseWithData<ProductResult>> DeserializeSingleApiResponse(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var ret = JsonConvert.DeserializeObject<ApiResponseWithData<ProductResult>>(responseContent);
        return ret;
    }

    public async Task ProductSeedDatabase()
    {
        if (ProductsLoad)
            return;

        var products = ProductTestData.GenerateProducts(25);

        foreach (var product in products)
        {
            var response = await Client.PostAsJsonAsync("/api/products", product);
            response.EnsureSuccessStatusCode();
        }
    }

    public void Dispose()
    {
        _client?.Dispose();
        _factory?.Dispose();
        GC.SuppressFinalize(this);
    }
}
