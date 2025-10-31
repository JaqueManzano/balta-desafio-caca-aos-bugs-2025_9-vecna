using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Products;
using BugStore.Application.Responses.Products;
using MediatR;

namespace BugStore.Application.Handlers.Products
{
    public class GetByIdProductHandler : IRequestHandler<GetByIdProductsRequest, GetByIdProductsResponse>
    {
        private readonly IProductsService _service;
        private readonly IMapper _mapper;

        public GetByIdProductHandler(IProductsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<GetByIdProductsResponse> Handle(GetByIdProductsRequest request, CancellationToken cancellationToken)
        {
            var (product, message) = await _service.GetByIdAsync(request.Id, cancellationToken);

            var dto = product != null ? _mapper.Map<ProductDto>(product) : null;

            return new GetByIdProductsResponse
            {
                Product = dto,
                Message = message
            };
        }
    }
}
