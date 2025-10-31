using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Customers;
using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Application.Handlers.Customers
{
    public class CreateCustomerHandler(ICustomerService _service, IMapper _mapper) : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                {
                    return new CreateCustomerResponse
                    {
                        Success = false,
                        Message = "Error: " + string.Join(", ", request.Notifications.Select(n => n.Message))
                    };
                }

                var (customer, success, message) = await _service.CreateCustomerAsync(
                    request.Name,
                    request.Email,
                    request.Phone,
                    request.BirthDate,
                    cancellationToken
                );

                var dto = _mapper.Map<CustomerDto>(customer);

                return new CreateCustomerResponse
                {
                    Customer = dto,
                    Success = success,
                    Message = message
                };
            }
            catch (Exception)
            {
                return new CreateCustomerResponse
                {
                    Customer = null,
                    Success = false,
                    Message = "The customer could not be registered."
                };
            }
        }
    }
}
