namespace BugStore.Application.UseCases.Orders.Search;

public interface ISearchOrdersUseCase
{
    Task<IEnumerable<Response>> ExecuteAsync(Request request, CancellationToken cancellationToken);
}
