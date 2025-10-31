using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Application.Requests.Customers;

public class GetByIdCustomerRequest : IRequest<GetByIdCustomerResponse>
{
    public Guid Id { get; set; }
}