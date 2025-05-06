using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth.AuthenticateUser;

public class AuthenticateUserProfileTests
{
    private readonly IMapper _mapper;

    public AuthenticateUserProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AuthenticateUserProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact(DisplayName = "Given User When mapping to AuthenticateUserResult Then should map correctly")]
    public void Map_User_To_AuthenticateUserResult_ShouldMapCorrectly()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            Phone = "+1234567890",
            Status = UserStatus.Active,
            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = _mapper.Map<AuthenticateUserResult>(user);

        // Assert
        Assert.Equal(user.Id, result.Id);
    }
}