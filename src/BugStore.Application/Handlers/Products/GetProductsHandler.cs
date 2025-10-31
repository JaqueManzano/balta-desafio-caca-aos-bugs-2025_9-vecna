using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Products;
using BugStore.Application.Responses.Products;
using MediatR;

namespace BugStore.Application.Handlers.Products
{
    public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        private readonly IProductsService _service;
        private readonly IMapper _mapper;

        public GetProductsHandler(IProductsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var (products, message) = await _service.GetAllAsync(cancellationToken);

            var dtoList = products.Any() ? _mapper.Map<List<ProductDto>>(products) : new List<ProductDto>();

            return new GetProductsResponse
            {
                Products = dtoList,
                Message = message,
                Success = dtoList?.Count > 0
            };
        }
    }
}
