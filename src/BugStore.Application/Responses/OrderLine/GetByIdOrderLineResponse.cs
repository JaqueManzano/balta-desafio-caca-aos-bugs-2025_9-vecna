namespace BugStore.Application.Responses.OrderLine;

public class GetByIdOrderLineResponse
{
    public Domain.Entities.OrderLine? OrderLine { get; set; }
    public string Message { get; set; } = string.Empty;
}

