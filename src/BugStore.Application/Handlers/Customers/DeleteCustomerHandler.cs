using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Customers;
using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Application.Handlers.Customers
{
    public class DeleteCustomerHandler(ICustomerService _service) : IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
    {
        public async Task<DeleteCustomerResponse> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var (success, message) = await _service.DeleteCustomerAsync(request.Id, cancellationToken);

                return new DeleteCustomerResponse
                {
                    Success = success,
                    Message = message
                };
            }
            catch (Exception)
            {
                return new DeleteCustomerResponse
                {
                    Success = false,
                    Message = "The customer could not be deleted."
                };
            }
        }
    }
}
