# Advanced Filtering, Ordering and Pagination - Documentation

## Purpose

This project implements a consistent and reusable pattern for filtering, ordering, and pagination in ASP.NET Core Web APIs. It ensures:

* Easy implementation of dynamic queries.
* Secure filtering and ordering based on allowed fields.
* DRY principles (Don't Repeat Yourself) using base classes.
* Input validation through FluentValidation.

## Folder Structure

* `Host`: API layer (controllers, middlewares, etc.)
* `Application`: Commands, queries, DTOs, validations.
* `Domain`: Core entities and contracts.
* `Common`: Shared logic, base classes, helpers.
* `Infrastructure/ORM`: EF Core context, repositories.
* `IoC`: Dependency injection configuration.

## Key Components

### `AbstractAdvancedFilter`

A base class for requests that include filtering, ordering and pagination.

```csharp
public abstract class AbstractAdvancedFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; }
    public Dictionary<string, string>? Filters { get; set; }
}
```

### `AbstractAdvancedFilterValidator<Entity, Request>`

Validates incoming requests:

* Ensures `PageNumber > 0` and `PageSize` is between 1 and 100.
* Ensures all provided filters match public properties of the target `Entity`.

### Filtering Syntax (Query String)

| Filter Type  | Example (Query String) | Result                   |
| ------------ | ---------------------- | ------------------------ |
| Equals       | `?name=Beer`           | `Name == "Beer"`         |
| Contains     | `?name=*bee*`          | `Name.Contains("bee")`   |
| Starts With  | `?name=bee*`           | `Name.StartsWith("bee")` |
| Ends With    | `?name=*beer`          | `Name.EndsWith("beer")`  |
| Greater Than | `?_minprice=10`        | `Price >= 10`            |
| Less Than    | `?_maxprice=100`       | `Price <= 100`           |

### Ordering Syntax (Query String)

* Use `_order` parameter with comma-separated fields.
* Example: `?_order=price desc,name asc`

### Pagination

Handled automatically with:

* `_page` (default: 1)
* `_size` (default: 10, max: 100)

## Example Endpoint

```csharp
[HttpGet]
public async Task<IActionResult> ListProducts(CancellationToken cancellationToken,
    [FromQuery] Dictionary<string, string>? filters = null)
{
    var request = new ListProductsRequest(filters);
    var validator = new ListProductsRequestValidator();
    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
        return BadRequest(validationResult.Errors);

    var command = _mapper.Map<ListProductsCommand>(request);
    var result = await _mediator.Send(command, cancellationToken);

    return OkPaginated(result);
}
```

## Developer Onboarding Checklist ✅

* Create a `Request` inheriting `AbstractAdvancedFilter`
* Create a `Validator` inheriting `AbstractAdvancedFilterValidator<YourEntity, YourRequest>`
* Implement a `Command` inheriting `AbstractAdvancedFilter`
* Use `CreatePaginatedListAsync(filter)` in repository
* Expose it through controller using `Dictionary<string,string>? filters`