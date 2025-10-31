namespace BugStore.Application.UseCases.Reports.RevenueByPeriod.Search
{
    public class Request
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? ProductTitle { get; set; }
        public decimal? ProductPriceStart { get; set; }
        public decimal? ProductPriceEnd { get; set; }
        public DateTime? CreatedAtStart { get; set; }
        public DateTime? CreatedAtEnd { get; set; }
        public DateTime? UpdatedAtStart { get; set; }
        public DateTime? UpdatedAtEnd { get; set; }
    }
}