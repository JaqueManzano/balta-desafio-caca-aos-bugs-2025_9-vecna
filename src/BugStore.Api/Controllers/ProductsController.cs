using BugStore.Application.Requests.Products;
using BugStore.Application.UseCases.Products.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISearchProductsUseCase _searchProductssUseCase;

        public ProductsController(IMediator mediator, ISearchProductsUseCase searchProductssUseCase)
        {
            _mediator = mediator;
            _searchProductssUseCase = searchProductssUseCase;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetByIdProductsRequest { Id = id });
            return response.Product is null ? NotFound(response.Message) : Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetProductsRequest());
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductsRequest request)
        {
            var response = await _mediator.Send(request);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateProductsRequest request)
        {
            var response = await _mediator.Send(request);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteProductsRequest { Id = id });
            return response.Success ? Ok(response) : NotFound(response.Message);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] Request request, CancellationToken cancellationToken)
        {
            var result = await _searchProductssUseCase.ExecuteAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}
