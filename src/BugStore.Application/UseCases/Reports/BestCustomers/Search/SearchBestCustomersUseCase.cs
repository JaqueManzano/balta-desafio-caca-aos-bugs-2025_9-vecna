using BugStore.Application.UseCases.Reports.BestCustomers.SearchCustomer;
using BugStore.Domain.Interfaces;
using System.Globalization;

namespace BugStore.Application.UseCases.Reports.BestCustomers.Search
{
    public class SearchBestCustomersUseCase : ISearchBestCustomersUseCase
    {
        private readonly IReportBestCustomerRepository _reportCustomerRepository;
        
        public SearchBestCustomersUseCase(IReportBestCustomerRepository reportCustomerRepository)
        {
            _reportCustomerRepository = reportCustomerRepository;
        }

        public async Task<IEnumerable<Response>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var reportData = await _reportCustomerRepository.SearchAsync(cancellationToken);

            return reportData.Select(r => new Response
            {
                CustomerName = r.CustomerName,
                CustomerEmail = r.CustomerEmail,
                TotalOrders = r.TotalOrders,
                SpentAmount = r.SpentAmount
            });
        }
    }
}
