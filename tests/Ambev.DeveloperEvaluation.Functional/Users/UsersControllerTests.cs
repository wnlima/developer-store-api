using System.Net.Http.Json;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using System.Net;
using Ambev.DeveloperEvaluation.Functional.Auxiliary;
using Ambev.DeveloperEvaluation.TestUtils.TestData;
using Newtonsoft.Json;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Functional.Users;

public class AuthControllerTests : IAsyncLifetime, IClassFixture<HttpClientFixture>
{
    private readonly HttpClientFixture _clientFixture;

    public AuthControllerTests(HttpClientFixture clientFixture)
    {
        _clientFixture = clientFixture;
        _clientFixture.UserSeed().GetAwaiter().GetResult();
    }


    public Task InitializeAsync()
        => Task.CompletedTask;

    public async Task DisposeAsync()
    {

    }

    [Fact(DisplayName = "CreateUser - Expect 201 Created on successful user creation")]
    public async Task CreateUser_Expect_201_Created_On_Successful_User_Creation()
    {
        // Arrange
        var data = CreateUserHandlerTestData.GenerateCreateUserCommand();
        data.Status = Domain.Enums.UserStatus.Active;
        data.Role = Domain.Enums.UserRole.Customer;

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Post, "/api/users", data, _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponseWithData<UserResponse>>(responseContent);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.NotNull(apiResponse.Data);
        Assert.NotEqual(Guid.Empty, apiResponse.Data.Id);
        Assert.NotEmpty(apiResponse.Data.Username);
        Assert.NotEmpty(apiResponse.Data.Phone);
    }

    [Fact(DisplayName = "CreateUser - Expect 400 Bad Request when username is missing")]
    public async Task CreateUser_Expect_400_Bad_Request_When_Username_Is_Missing()
    {
        // Arrange
        var data = CreateUserHandlerTestData.GenerateCreateUserCommand();
        data.Username = "";

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Post, "/api/users", data, _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(DisplayName = "GetUser - Expect 200 OK on successful user retrieval")]
    public async Task GetUser_Expect_200_OK_On_Successful_User_Retrieval()
    {
        // Arrange
        // First, create a user to retrieve
        var createData = CreateUserHandlerTestData.GenerateCreateUserCommand();
        createData.Status = Domain.Enums.UserStatus.Active;
        createData.Role = Domain.Enums.UserRole.Customer;

        var createResponse = await _clientFixture.RequestSend(HttpMethod.Post, "/api/users", createData, _clientFixture.ManagerUser.Token);
        var createResponseContent = await createResponse.Content.ReadAsStringAsync();
        var createApiResponse = JsonConvert.DeserializeObject<ApiResponseWithData<UserResponse>>(createResponseContent);

        Assert.NotNull(createApiResponse);
        Assert.True(createApiResponse.Success);
        Assert.NotNull(createApiResponse.Data);
        Assert.NotEqual(Guid.Empty, createApiResponse.Data.Id);

        var userId = createApiResponse.Data.Id;

        // Act
        var getResponse = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/users/{userId}", _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getResponseContent = await getResponse.Content.ReadAsStringAsync();
        var getApiResponse = JsonConvert.DeserializeObject<ApiResponseWithData<GetUserResponse>>(getResponseContent)!;

        Assert.NotNull(getApiResponse);
        Assert.True(getApiResponse.Success);
        Assert.NotNull(getApiResponse.Data);
        Assert.Equal(userId, getApiResponse.Data.Id);
    }

    [Fact(DisplayName = "GetUser - Expect 400 Bad Request when user ID is invalid")]
    public async Task GetUser_Expect_400_Bad_Request_When_User_ID_Is_Invalid()
    {
        // Arrange
        var invalidUserId = "invalid-guid";

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/users/{invalidUserId}", _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(DisplayName = "GetUser - Expect 404 Not Found when user ID does not exist")]
    public async Task GetUser_Expect_404_Not_Found_When_User_ID_Does_Not_Exist()
    {
        // Arrange
        var nonExistentUserId = Guid.NewGuid();

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Get, $"/api/users/{nonExistentUserId}", _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact(DisplayName = "DeleteUser - Expect 200 OK on successful user deletion")]
    public async Task DeleteUser_Expect_200_OK_On_Successful_User_Deletion()
    {
        // Arrange
        var createData = CreateUserHandlerTestData.GenerateCreateUserCommand();
        createData.Status = Domain.Enums.UserStatus.Active;
        createData.Role = Domain.Enums.UserRole.Customer;

        var createResponse = await _clientFixture.RequestSend(HttpMethod.Post, "/api/users", createData, _clientFixture.ManagerUser.Token);
        var createResponseContent = await createResponse.Content.ReadAsStringAsync();
        var createApiResponse = JsonConvert.DeserializeObject<ApiResponseWithData<UserResponse>>(createResponseContent);

        Assert.NotNull(createApiResponse);
        Assert.True(createApiResponse.Success);
        Assert.NotNull(createApiResponse.Data);
        Assert.NotEqual(Guid.Empty, createApiResponse.Data.Id);

        var userId = createApiResponse.Data.Id;

        // Act
        var deleteResponse = await _clientFixture.RequestSend(HttpMethod.Delete, $"/api/users/{userId}", _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

        var deleteResponseContent = await deleteResponse.Content.ReadAsStringAsync();
        var deleteApiResponse = JsonConvert.DeserializeObject<ApiResponse>(deleteResponseContent);

        Assert.NotNull(deleteApiResponse);
        Assert.True(deleteApiResponse.Success);
    }

    [Fact(DisplayName = "DeleteUser - Expect 400 Bad Request when user ID is invalid")]
    public async Task DeleteUser_Expect_400_Bad_Request_When_User_ID_Is_Invalid()
    {
        // Arrange
        var invalidUserId = "invalid-guid";

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Delete, $"/api/users/{invalidUserId}", _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(DisplayName = "DeleteUser - Expect 404 Not Found when user ID does not exist")]
    public async Task DeleteUser_Expect_404_Not_Found_When_User_ID_Does_Not_Exist()
    {
        // Arrange
        var nonExistentUserId = Guid.NewGuid();

        // Act
        var response = await _clientFixture.RequestSend(HttpMethod.Delete, $"/api/users/{nonExistentUserId}", _clientFixture.ManagerUser.Token);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}