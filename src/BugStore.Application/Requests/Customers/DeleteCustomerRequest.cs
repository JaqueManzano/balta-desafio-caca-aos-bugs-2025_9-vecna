using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Application.Requests.Customers;

public class DeleteCustomerRequest : IRequest<DeleteCustomerResponse>
{
    public Guid Id { get; set; }
}
