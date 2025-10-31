using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Customers;
using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers
{
    public class GetCustomerHandler(ICustomerService _service, IMapper _mapper) : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
    {
        public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var (customers, success, message) = await _service.GetAllCustomersAsync(cancellationToken);

                var dto = _mapper.Map<List<CustomerDto>>(customers);

                return new GetCustomerResponse
                {
                    Customers = dto,
                    Success = success,
                    Message = message
                };
            }
            catch (Exception e)
            {
                return new GetCustomerResponse
                {
                    Customers = new(),
                    Success = false,
                    Message = "It was not possible to search for registered customers."
                };
            }
        }
    }
}
