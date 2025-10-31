using BugStore.Application.UseCases.Reports.BestCustomers.SearchCustomer;

namespace BugStore.Application.UseCases.Reports.BestCustomers.Search
{
    public interface ISearchBestCustomersUseCase
    {
        Task<IEnumerable<Response>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
