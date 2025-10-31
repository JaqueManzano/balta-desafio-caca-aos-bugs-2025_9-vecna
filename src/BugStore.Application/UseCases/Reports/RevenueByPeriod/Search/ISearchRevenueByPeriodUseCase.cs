namespace BugStore.Application.UseCases.Reports.RevenueByPeriod.Search
{
    public interface ISearchRevenueByPeriodUseCase
    {
        Task<IEnumerable<Response>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
