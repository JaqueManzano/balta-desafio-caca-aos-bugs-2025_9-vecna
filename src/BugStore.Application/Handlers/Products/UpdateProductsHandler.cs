using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Products;
using BugStore.Application.Responses.Products;
using BugStore.Domain.Entities;
using MediatR;

namespace BugStore.Application.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductsRequest, UpdateProductsResponse>
    {
        private readonly IProductsService _service;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<UpdateProductsResponse> Handle(UpdateProductsRequest request, CancellationToken cancellationToken)
        {
            var productToUpdate = new Product
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Slug = request.Slug,
                Price = request.Price ?? default
            };

            var (product, success, message) = await _service.UpdateAsync(productToUpdate, cancellationToken);

            var dto = product != null ? _mapper.Map<ProductDto>(product) : null;

            return new UpdateProductsResponse
            {
                Product = dto,
                Success = success,
                Message = message
            };
        }
    }
}
