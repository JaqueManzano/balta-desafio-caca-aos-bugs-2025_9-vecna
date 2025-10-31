using BugStore.Application.UseCases.Reports.BestCustomers.Search;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportBestCustomerController : ControllerBase
    {
        private readonly ISearchBestCustomersUseCase _searchBestCustomersUseCase;

        public ReportBestCustomerController(ISearchBestCustomersUseCase searchBestCustomersUseCase)
        {
            _searchBestCustomersUseCase = searchBestCustomersUseCase;
        }

        [HttpGet("BestCustomer")]
        public async Task<IActionResult> Search(CancellationToken cancellationToken)
        {
            var result = await _searchBestCustomersUseCase.ExecuteAsync(cancellationToken);
            return Ok(result);
        }
    }
}

