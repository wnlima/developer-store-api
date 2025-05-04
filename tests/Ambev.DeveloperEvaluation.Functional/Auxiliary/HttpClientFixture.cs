using System.Net.Http.Json;
using System.Text;
using Ambev.DeveloperEvaluation.ORM;
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
    private bool _productSeedLoaded;
    private bool _saleSeedLoaded;
    private bool _userSeedLoaded;
    public readonly DefaultContext DbContext;
    public readonly IServiceScope _scope;

    public HttpClientFixture()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _scope = Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<DefaultContext>();
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

    public async Task<ApiResponseWithData<T>> DeserializeApiResponseWithData<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var ret = JsonConvert.DeserializeObject<ApiResponseWithData<T>>(responseContent);
        return ret;
    }

    public async Task BasicDataSeed()
    {
        await UserSeed();
        await ProductSeed();
    }

    public async Task UserSeed()
    {
        if (_userSeedLoaded)
            return;

        var data = CreateUserHandlerTestData.GenerateValidCommand();
        data.Status = Domain.Enums.UserStatus.Active;
        data.Role = Domain.Enums.UserRole.Customer;

        var response = await Client.PostAsJsonAsync("/api/users", data);
        response.EnsureSuccessStatusCode();

        data = CreateUserHandlerTestData.GenerateValidCommand();
        data.Status = Domain.Enums.UserStatus.Active;
        data.Role = Domain.Enums.UserRole.Admin;

        response = await Client.PostAsJsonAsync("/api/users", data);
        response.EnsureSuccessStatusCode();

        data = CreateUserHandlerTestData.GenerateValidCommand();
        data.Status = Domain.Enums.UserStatus.Active;
        data.Role = Domain.Enums.UserRole.Manager;

        response = await Client.PostAsJsonAsync("/api/users", data);
        response.EnsureSuccessStatusCode();

        _userSeedLoaded = true;
    }

    public async Task ProductSeed()
    {
        if (_productSeedLoaded)
            return;

        var items = ProductTestData.GenerateProducts(25);

        foreach (var product in items)
        {
            var response = await Client.PostAsJsonAsync("/api/products", product);
            response.EnsureSuccessStatusCode();
        }

        _productSeedLoaded = true;
    }

    public async Task SaleSeed()
    {
        if (_saleSeedLoaded)
            return;

        var items = SaleTestData.Generate(25);

        foreach (var value in items)
        {
            var response = await Client.PostAsJsonAsync("/api/sales", value);
            response.EnsureSuccessStatusCode();
        }

        _saleSeedLoaded = true;
    }

    public void Dispose()
    {
        _scope.Dispose();
        _client?.Dispose();
        _factory?.Dispose();
        GC.SuppressFinalize(this);
    }
}
