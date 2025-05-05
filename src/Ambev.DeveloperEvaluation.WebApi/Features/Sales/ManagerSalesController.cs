using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Validators;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ManagerSalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ManagerSalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [Authorize(Policy = "ManagerOnly")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var command = _mapper.Map<CreateSaleCommand>(request);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return this.FailedResponse(validationResult.Errors);

        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<SaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<SaleResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a sale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = "ManagerOnly")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new ManagerGetSaleByIdRequest(id);
        var validator = new ManagerGetSaleByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return this.FailedResponse(validationResult.Errors);

        var command = _mapper.Map<GetSaleByIdCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<SaleResponse>(response), "Sale retrieved successfully");
    }

    /// <summary>
    /// Cancels a sale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the sale was successfully canceled</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = "ManagerOnly")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new CancelSaleRequest(id);
        var validator = new CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return this.FailedResponse(validationResult.Errors);

        var command = _mapper.Map<ManagerCancelSaleCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Sucess("Sale deleted successfully");
    }

    [HttpGet]
    [Authorize(Policy = "ManagerOnly")]
    [SwaggerOperation(
    Summary = "List sales with advanced filtering, ordering and pagination",
    Description = """
    Supports dynamic filtering through query string parameters.

    | Filter Type  | Example (Query String)
    | ------------ | ----------------------
    | Equals       | `?name=Beer`          
    | Contains     | `?name=*bee*`         
    | Starts With  | `?name=bee*`          
    | Ends With    | `?name=*beer`         
    | Greater Than | `?_minprice=10`       
    | Less Than    | `?_maxprice=100`      

    #### Ordering
    Use `_order` query param with comma-separated fields.
    Example: `?_order=price desc,name asc`

    #### Pagination
    Supported via:
    - `_size` (default: 1)
    - `_page` (default: 10)
    """
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Successful request", typeof(PaginatedList<ListSalesResult>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation error", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No results found", typeof(ApiResponse))]
    [ProducesResponseType(typeof(PaginatedList<ListSalesResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> List(CancellationToken cancellationToken,
    [FromQuery] Dictionary<string, string>? filters = null)
    {
        var request = new ListSalesRequest(filters);
        var validator = new ListSalesRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return this.FailedResponse(validationResult.Errors);

        var command = _mapper.Map<ListSalesCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return OkPaginated(result);
    }
}