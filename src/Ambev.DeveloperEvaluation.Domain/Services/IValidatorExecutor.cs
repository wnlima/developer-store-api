using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IValidatorService
{
    Task<IEnumerable<ValidationErrorDetail>> ValidateAsync<T>(T instance);
}
