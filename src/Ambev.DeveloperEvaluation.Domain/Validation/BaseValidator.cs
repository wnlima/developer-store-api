using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    public ValidationResultDetail ValidateDetail(T instance)
    {
        var result = this.Validate(instance);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
