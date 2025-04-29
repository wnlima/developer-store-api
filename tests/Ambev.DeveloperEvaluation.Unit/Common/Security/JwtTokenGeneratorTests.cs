using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.Security;

public class JwtTokenGeneratorTests
{
    private readonly IConfiguration _configuration;
    private readonly JwtTokenGenerator _tokenGenerator;
    private const string SECRET_KEY = "YourSuperSecretKeyForJwtTokenGenerationThatShouldBeAtLeast32BytesLong";

    public JwtTokenGeneratorTests()
    {
        _configuration = Substitute.For<IConfiguration>();
        _configuration["Jwt:SecretKey"].Returns(SECRET_KEY);
        _tokenGenerator = new JwtTokenGenerator(_configuration);
    }

    [Fact(DisplayName = "Given a user When generating token Then should return a valid token")]
    public void GenerateToken_Should_Return_ValidToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            Role = UserRole.Admin,
            Status = UserStatus.Active
        };

        // Act
        string token = _tokenGenerator.GenerateToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(SECRET_KEY));
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        Assert.Contains(jwtToken.Claims, x => x.Value == user.Id.ToString());
        Assert.Contains(jwtToken.Claims, x => x.Value == user.Username);
        Assert.Contains(jwtToken.Claims, x => x.Value == user.Role.ToString());
    }
}