using BugStore.Domain.Interfaces;
using BugStore.Domain.Models.Reports;
using BugStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Repositories
{
    public class ReportBestCustomerRepository : IReportBestCustomerRepository
    {
        private readonly AppDbContext _context;

        public ReportBestCustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BestCustomers>> SearchAsync(CancellationToken cancellationToken)
        {
            var data = await _context
                .Set<BestCustomers>()
                .FromSqlRaw("SELECT * FROM vw_report_best_customers")
                .ToListAsync(cancellationToken);

            return data;
        }
    }
}
