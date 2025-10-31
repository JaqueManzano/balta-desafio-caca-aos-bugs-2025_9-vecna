using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Customers;
using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Application.Handlers.Customers
{
    public class UpdateCustomerHandler(ICustomerService _service, IMapper _mapper) : IRequestHandler<UpdateCustomerRequest, UpdateCustomerResponse>
    {
        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var (customer, success, message) = await _service.UpdateCustomerAsync(
                    request.Id,
                    request.Name,
                    request.Email,
                    request.Phone,
                    request.BirthDate,
                    cancellationToken
                );

                var dto = _mapper.Map<CustomerDto>(customer);

                return new UpdateCustomerResponse
                {
                    Customer = dto,
                    Success = success,
                    Message = message
                };
            }
            catch (Exception)
            {
                return new UpdateCustomerResponse
                {
                    Customer = null,
                    Success = false,
                    Message = "Client could not be updated."
                };
            }
        }
    }
}
