using System.Text;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Ambev.DeveloperEvaluation.Functional.Auxiliary;

public sealed class HttpClientFixture : IDisposable
{
    internal const string COLLECTION_NAME = "HttpClient collection";
    private readonly CustomWebApplicationFactory<Program> _factory;
    private HttpClient? _client;

    public HttpClientFixture()
    {
        _factory = new CustomWebApplicationFactory<Program>();
    }

    public void SetDIBehaviors(params Action<IServiceCollection>[] behaviors)
    {
        _factory.SetDIBehaviors(behaviors);
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

    public void Dispose()
    {
        _client?.Dispose();
        _factory?.Dispose();
        GC.SuppressFinalize(this);
    }
}
