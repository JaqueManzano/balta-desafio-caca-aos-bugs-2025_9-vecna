using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Application.Requests.Customers;

public class GetCustomerRequest : IRequest<GetCustomerResponse>
{
}