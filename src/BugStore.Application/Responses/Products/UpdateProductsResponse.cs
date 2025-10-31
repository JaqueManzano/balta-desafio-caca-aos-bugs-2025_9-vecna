using BugStore.Application.Dtos;

namespace BugStore.Application.Responses.Products;

public class UpdateProductsResponse
{
    public ProductDto? Product { get; set; }
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
}

