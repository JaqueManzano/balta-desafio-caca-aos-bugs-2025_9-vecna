namespace BugStore.Application.UseCases.Customers.Search;

public interface ISearchCustomersUseCase
{
    Task<IEnumerable<Response>> ExecuteAsync(Request request, CancellationToken cancellationToken);
}
