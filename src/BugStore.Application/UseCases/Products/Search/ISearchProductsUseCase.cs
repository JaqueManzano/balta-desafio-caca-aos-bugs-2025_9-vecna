namespace BugStore.Application.UseCases.Products.Search;

public interface ISearchProductsUseCase
{
    Task<IEnumerable<Response>> ExecuteAsync(Request request, CancellationToken cancellationToken);
}
