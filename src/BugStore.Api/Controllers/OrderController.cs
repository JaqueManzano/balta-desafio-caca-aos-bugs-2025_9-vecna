using BugStore.Application.Requests.Orders;
using BugStore.Application.UseCases.Orders.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ISearchOrdersUseCase _searchOrdersUseCase;
        public OrdersController(IMediator mediator, ISearchOrdersUseCase searchOrdersUseCase)
        {
            _mediator = mediator;
            _searchOrdersUseCase = searchOrdersUseCase;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOrdersRequest request)
        {
            var response = await _mediator.Send(request);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetByIdOrdersRequest { Id = id });
            return response.Order is null ? NotFound(response.Message) : Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] Request request, CancellationToken cancellationToken)
        {
            var result = await _searchOrdersUseCase.ExecuteAsync(request, cancellationToken);
            return Ok(result);
        }

    }
}
