
using BugStore.Domain.Interfaces;
using System.Globalization;

namespace BugStore.Application.UseCases.Reports.RevenueByPeriod.Search
{
    public class SearchRevenueByPeriodUseCase : ISearchRevenueByPeriodUseCase
    {
        private readonly IReportRevenueByPeriodRepository _reportRevenueByPeriod;

        public SearchRevenueByPeriodUseCase(IReportRevenueByPeriodRepository reportRevenueByPeriod)
        {
            _reportRevenueByPeriod = reportRevenueByPeriod;
        }

        public async Task<IEnumerable<Response>> ExecuteAsync(CancellationToken cancellationToken)
        {

            var reportData = await _reportRevenueByPeriod.SearchAsync(cancellationToken);

            return reportData.Select(r => new Response
            {
                Year = r.Year,
                Month = r.Month,
                TotalOrders = r.TotalOrders,
                TotalRevenue = r.TotalRevenue
            });
        }
    }
}
