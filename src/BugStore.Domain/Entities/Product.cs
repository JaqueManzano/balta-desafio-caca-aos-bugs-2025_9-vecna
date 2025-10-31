using System.ComponentModel.DataAnnotations.Schema;

namespace BugStore.Domain.Entities;

[Table("Products")]
public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }
}