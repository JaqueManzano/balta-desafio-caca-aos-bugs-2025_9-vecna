using BugStore.Domain.Models.Reports;

namespace BugStore.Domain.Interfaces
{
    public interface IReportBestCustomerRepository
    {
        Task<List<BestCustomers>> SearchAsync(CancellationToken cancellationToken);
    }
}
