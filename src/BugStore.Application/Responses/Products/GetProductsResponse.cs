using BugStore.Application.Dtos;

namespace BugStore.Application.Responses.Products;

public class GetProductsResponse
{
    public List<ProductDto> Products { get; set; } = new();
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
