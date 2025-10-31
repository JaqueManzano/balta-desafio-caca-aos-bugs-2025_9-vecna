using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;
using BugStore.Domain.Models.Reports;
using BugStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Repositories
{
    public class ReportRevenueByPeriodRepository : IReportRevenueByPeriodRepository
    {
        private readonly AppDbContext _context;

        public ReportRevenueByPeriodRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RevenueByPeriodResult>> SearchAsync(CancellationToken cancellationToken)
        {
            var data = await _context
            .Set<RevenueByPeriodResult>()
            .FromSqlRaw("SELECT * FROM vw_report_revenue_by_period").ToListAsync(cancellationToken);

            return data;
            ;
        }
    }
}
