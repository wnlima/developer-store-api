using System.Net.Http.Headers;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.TestUtils.TestData;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public AuthenticateUserResult ManagerUser;
    public AuthenticateUserResult AdminUser;
    public AuthenticateUserResult CustomerUser;
    private bool _userSeedLoaded;
    public readonly DefaultContext DbContext;
    public readonly IServiceScope _scope;

    public HttpClientFixture()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _scope = Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<DefaultContext>();
    }

    HttpClient Client
    {
        get
        {
            _client ??= _factory.CreateClient();
            return _client;
        }
    }

    public IServiceProvider Services => _factory.Services;

    public IConfigurationRoot Configuration => _factory.Configuration;



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

    public async Task<HttpResponseMessage> RequestSend(HttpMethod httpMethod, string requestUri, string token = "")
    {
        var request = new HttpRequestMessage(httpMethod, requestUri);

        if (string.IsNullOrEmpty(token))
            token = CustomerUser.Token;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await Client.SendAsync(request);
    }

    public async Task<HttpResponseMessage> RequestSend<TValue>(HttpMethod httpMethod, string requestUri, TValue value = null, string token = "") where TValue : class
    {
        var request = new HttpRequestMessage(httpMethod, requestUri);

        if (string.IsNullOrEmpty(token))
            token = CustomerUser.Token;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        if (value != null)
        {
            JsonContent content = JsonContent.Create(value, mediaType: null);
            request.Content = content;
        }

        return await Client.SendAsync(request);
    }

    public async Task UserSeed()
    {
        if (_userSeedLoaded)
            return;

        var mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();

        var data = CreateUserHandlerTestData.GenerateCreateUserCommand();
        data.Status = Domain.Enums.UserStatus.Active;
        data.Role = Domain.Enums.UserRole.Customer;
        await mediator.Send(data);

        var auth = new AuthenticateUserCommand();
        auth.Email = data.Email;
        auth.Password = data.Password;

        CustomerUser = await mediator.Send(auth);

        data = CreateUserHandlerTestData.GenerateCreateUserCommand();
        data.Status = Domain.Enums.UserStatus.Active;
        data.Role = Domain.Enums.UserRole.Admin;
        await mediator.Send(data);

        auth = new AuthenticateUserCommand();
        auth.Email = data.Email;
        auth.Password = data.Password;

        AdminUser = await mediator.Send(auth);

        data = CreateUserHandlerTestData.GenerateCreateUserCommand();
        data.Status = Domain.Enums.UserStatus.Active;
        data.Role = Domain.Enums.UserRole.Manager;
        await mediator.Send(data);

        auth = new AuthenticateUserCommand();
        auth.Email = data.Email;
        auth.Password = data.Password;

        ManagerUser = await mediator.Send(auth);

        _userSeedLoaded = true;
    }

    public async Task ProductSeed()
    {
        if (_productSeedLoaded)
            return;

        var items = ProductTestData.GenerateProducts(25);

        var bFirst = false;
        foreach (var product in items)
        {
            if (!bFirst)
                product.QuantityInStock = 100;

            var response = await RequestSend(HttpMethod.Post, "/api/products", product, ManagerUser.Token);
            response.EnsureSuccessStatusCode();
        }

        _productSeedLoaded = true;
    }

    public async Task SaleSeed()
    {
        if (_saleSeedLoaded)
            return;

        var items = SaleTestData.Generate(11);
        var products = await DbContext.Products.AsNoTracking().ToListAsync();
        var pCount = products.Count;
        Random random = new();

        foreach (var sale in items)
        {
            var pIdx = random.Next(pCount);
            var product = products[pIdx];
            var saleItem = new CreateSaleItemRequest();
            saleItem.ProductId = product.Id;
            saleItem.Quantity = 1;
            sale.SaleItems = [saleItem];

            var response = await RequestSend(HttpMethod.Post, "/api/sales", sale);
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
