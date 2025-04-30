using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<UserEntity>
{
    public bool IsSatisfiedBy(UserEntity user)
    {
        return user.Status == UserStatus.Active;
    }
}
