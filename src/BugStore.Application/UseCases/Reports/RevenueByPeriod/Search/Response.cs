namespace BugStore.Application.UseCases.Reports.RevenueByPeriod.Search;

public class Response
{
    public int Year { get; set; }
    public string Month { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
}