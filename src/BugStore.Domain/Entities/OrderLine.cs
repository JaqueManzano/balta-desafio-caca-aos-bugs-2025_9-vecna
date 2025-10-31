using System.ComponentModel.DataAnnotations.Schema;

namespace BugStore.Domain.Entities;

[Table("OrderLines")]

public class OrderLine
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}