using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Customers;
using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers
{
    public class GetByIdCustomerHandler(
        ICustomerService _service,
        IMapper _mapper)
        : IRequestHandler<GetByIdCustomerRequest, GetByIdCustomerResponse>
    {
        public async Task<GetByIdCustomerResponse> Handle(GetByIdCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var (customer, message) = await _service.GetCustomerByIdAsync(request.Id, cancellationToken);

                if (customer == null)
                    return new GetByIdCustomerResponse { Message = message };

                var dto = _mapper.Map<CustomerDto>(customer);

                return new GetByIdCustomerResponse
                {
                    Customer = dto,
                    Message = message
                };
            }
            catch (Exception)
            {
                return new GetByIdCustomerResponse
                {
                    Message = "The customer could not be consulted."
                };
            }
        }
    }
}
