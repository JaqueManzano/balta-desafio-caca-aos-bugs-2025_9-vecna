using BugStore.Application.Responses.Customers;
using MediatR;

namespace BugStore.Application.Requests.Customers;

public class UpdateCustomerRequest : IRequest<UpdateCustomerResponse>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
}

