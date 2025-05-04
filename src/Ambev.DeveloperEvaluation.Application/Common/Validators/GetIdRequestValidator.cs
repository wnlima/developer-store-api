using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

public class GetIdRequestValidator<T> : AbstractValidator<T> where T : class, IIdentifier
{
    protected virtual string Message => "ID is required";

    public GetIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(Message);
    }
}