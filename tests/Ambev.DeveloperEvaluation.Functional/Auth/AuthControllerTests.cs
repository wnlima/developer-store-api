using System.Net.Http.Json;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.WebApi.Common;
using System.Net;
using Ambev.DeveloperEvaluation.Functional.Auxiliary;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Ambev.DeveloperEvaluation.TestUtils.TestData;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Newtonsoft.Json;
using Ambev.DeveloperEvaluation.TestUtils;

namespace Ambev.DeveloperEvaluation.Functional.Auth;
public class AuthControllerTests : IAsyncLifetime, IClassFixture<HttpClientFixture>
{
    private readonly HttpClientFixture _clientFixture;

    public AuthControllerTests(HttpClientFixture clientFixture)
    {
        _clientFixture = clientFixture;
    }


    public Task InitializeAsync()
        => Task.CompletedTask;

    public async Task DisposeAsync()
    {

    }

    [Fact(DisplayName = "AuthenticateUser - Expect 200 OK on successful authentication")]
    public async Task AuthenticateUser_Expect_200_OK_On_Successful_Authentication()
    {
        // Arrange
        var newUser = CreateUserHandlerTestData.GenerateValidCommand();
        newUser.Status = Domain.Enums.UserStatus.Active;
        newUser.Role = Domain.Enums.UserRole.Customer;

        var data = new AuthenticateUserRequest
        {
            Email = newUser.Email,
            Password = newUser.Password,
        };

        newUser.Email = data.Email;
        newUser.Password = data.Password;

        using var scope = _clientFixture.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var cancellationTokenSource = new CancellationTokenSource();
        var newUserResponse = await mediator.Send(newUser, cancellationTokenSource.Token);

        // Act
        var response = await _clientFixture.Client.PostAsJsonAsync("/api/auth/login", data);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponseWithData<AuthenticateUserResult>>(responseContent);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.NotEmpty(apiResponse.Data.Token);
    }

    [Fact(DisplayName = "AuthenticateUser - Expect 400 Bad Request when email is missing")]
    public async Task AuthenticateUser_Expect_400_Bad_Request_When_Email_Is_Missing()
    {
        // Arrange
        var data = new AuthenticateUserRequest
        {
            Password = "ValidPassword123!"
        };

        // Act
        var response = await _clientFixture.Client.PostAsJsonAsync("/api/auth/login", data);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(DisplayName = "AuthenticateUser - Expect 400 Bad Request when password is missing")]
    public async Task AuthenticateUser_Expect_400_Bad_Request_When_Password_Is_Missing()
    {
        // Arrange
        var data = new AuthenticateUserRequest
        {
            Email = "test@example.com"
        };

        // Act
        var response = await _clientFixture.Client.PostAsJsonAsync("/api/auth/login", data);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(DisplayName = "AuthenticateUser - Expect 401 Unauthorized when credentials are invalid")]
    public async Task AuthenticateUser_Expect_401_Unauthorized_When_Credentials_Are_Invalid()
    {
        // Arrange
        var data = new AuthenticateUserRequest
        {
            Email = "invalid@example.com",
            Password = "InvalidPassword"
        };

        // Act
        var response = await _clientFixture.Client.PostAsJsonAsync("/api/auth/login", data);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
