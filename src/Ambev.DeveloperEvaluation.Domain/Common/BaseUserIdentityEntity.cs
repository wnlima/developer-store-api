using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

/// <summary>
/// Defines the contract for representing a user's identity within the system.
/// This class is used to access the user's unique identifier.
/// </summary>
public class BaseUserIdentityEntity : IIdentifier
{
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// This property is used to identify the related user in other entities.
    /// </summary>
    public Guid CustomerId { get; set; } = Guid.Empty;

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync(IValidatorService validator)
    {
        return validator.ValidateAsync(this);
    }
}