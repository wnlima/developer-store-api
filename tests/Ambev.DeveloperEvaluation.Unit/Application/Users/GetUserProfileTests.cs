using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

public class GetUserProfileTests
{
    private readonly IMapper _mapper;

    public GetUserProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact(DisplayName = "Given User When mapping to GetUserResult Then should map correctly")]
    public void Map_User_To_GetUserResult_ShouldMapCorrectly()
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
        var result = _mapper.Map<GetUserResult>(user);

        // Assert
        result.Should().BeEquivalentTo(new GetUserResult
        {
            Id = user.Id,
            Email = user.Email,
            Phone = user.Phone,
            Status = user.Status,
            Role = user.Role
        });
    }
}