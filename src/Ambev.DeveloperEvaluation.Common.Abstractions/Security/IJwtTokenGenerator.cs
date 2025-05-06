using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Common.Abstractions.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(IUser user);
}
