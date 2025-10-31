namespace BugStore.Application.UseCases.Customers.Search;

public class Request 
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}