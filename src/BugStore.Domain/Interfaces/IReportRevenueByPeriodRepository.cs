using BugStore.Domain.Models.Reports;

namespace BugStore.Domain.Interfaces
{
    public interface IReportRevenueByPeriodRepository
    {
        Task<List<RevenueByPeriodResult>> SearchAsync(CancellationToken cancellationToken);
    }
}
