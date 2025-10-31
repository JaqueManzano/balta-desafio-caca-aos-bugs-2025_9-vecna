using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Products;
using BugStore.Application.Responses.Products;
using BugStore.Domain.Entities;
using MediatR;

namespace BugStore.Application.Handlers.Products
{
    public class CreateProductsHandler(IProductsService _service, IMapper _mapper)
        : IRequestHandler<CreateProductsRequest, CreateProductsResponse>
    {
        public async Task<CreateProductsResponse> Handle(CreateProductsRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Slug = request.Slug,
                Price = request.Price
            };

            var (createdProduct, success, message) = await _service.CreateAsync(product, cancellationToken);

            var dto = createdProduct != null ? _mapper.Map<ProductDto>(createdProduct) : null;

            return new CreateProductsResponse
            {
                Product = dto,
                Success = success,
                Message = message
            };
        }

    }
}
