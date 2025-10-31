namespace BugStore.Application.Responses.OrderLine;

public class CreateOrderLineResponse
{
    public Domain.Entities.OrderLine? OrderLine { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}

