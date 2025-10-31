using BugStore.Application.Responses.Orders;
using MediatR;

namespace BugStore.Application.Requests.Orders;

public class GetByIdOrdersRequest : IRequest<GetByIdOrdersResponse>
{
    public Guid Id { get; set; }
}

