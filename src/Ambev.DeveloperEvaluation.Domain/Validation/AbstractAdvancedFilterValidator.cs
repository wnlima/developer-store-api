using System.Reflection;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public abstract class AbstractAdvancedFilterValidator<Entity, Request> : AbstractValidator<Request> where Request : AbstractAdvancedFilter
{
    private readonly HashSet<string> AllowedProperties = typeof(Entity)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Select(p => p.Name.ToLower())
        .ToHashSet();

    public AbstractAdvancedFilterValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page must be greater than zero");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100");

        When(x => x.Filters != null && x.Filters!.Count > 0, () =>
        {
            RuleForEach(x => x.Filters!).Must((request, kvp) =>
            {
                var cleanKey = CleanFieldName(kvp.Key);
                return AllowedProperties.Contains(cleanKey.ToLower());
            })
            .WithMessage(x => $"One or more filter fields are invalid. Allowed: {string.Join(", ", AllowedProperties)}");
        });
    }

    private string CleanFieldName(string input)
    {
        return input
            .Replace("_min", "", StringComparison.OrdinalIgnoreCase)
            .Replace("_max", "", StringComparison.OrdinalIgnoreCase)
            .Replace("_like", "", StringComparison.OrdinalIgnoreCase)
            .Replace("_in", "", StringComparison.OrdinalIgnoreCase)
            .Replace("_start", "", StringComparison.OrdinalIgnoreCase)
            .Replace("_end", "", StringComparison.OrdinalIgnoreCase);
    }
}