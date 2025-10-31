namespace BugStore.Application.UseCases.Products.Search;

public class Request
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public decimal? Price { get; set; }
}