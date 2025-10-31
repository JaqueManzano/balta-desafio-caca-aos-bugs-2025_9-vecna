namespace BugStore.Domain.Models.Reports
{
    public class BestCustomers
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public int TotalOrders { get; set; } = 0;
        public decimal SpentAmount { get; set; } = 0;
    }
}
