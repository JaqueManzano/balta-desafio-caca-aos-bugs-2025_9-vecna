using BugStore.Domain.Interfaces;

namespace BugStore.Application.UseCases.Customers.Search;

public class SearchCustomersUseCase : ISearchCustomersUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public SearchCustomersUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Response>> ExecuteAsync(Request request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.SearchAsync(request.Name, request.Email, request.Phone, cancellationToken);

        return customers.Select(c => new Response
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone
        });
    }
}
