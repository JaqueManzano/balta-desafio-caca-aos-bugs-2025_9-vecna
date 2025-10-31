using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Orders;
using BugStore.Application.Responses.Orders;
using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;

namespace BugStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly ICustomerService _customerService;
    private readonly IProductsService _productService;
    private readonly IOrderRepository _orderRepository;

    public OrderService(
        ICustomerService customerService,
        IProductsService productService,
        IOrderRepository orderRepository)
    {
        _customerService = customerService;
        _productService = productService;
        _orderRepository = orderRepository;
    }

    public async Task<GetByIdOrdersResponse> GetOrderByIdAsync(GetByIdOrdersRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

        if (order == null)
            return new GetByIdOrdersResponse
            {
                Order = null,
                Message = "Order not found."
            };

        return new GetByIdOrdersResponse
        {
            Order = order,
            Message = "Order found."
        };
    }

    public async Task<CreateOrdersResponse> CreateOrderAsync(CreateOrdersRequest request, CancellationToken cancellationToken)
    {
        var customerResponse = await _customerService.GetCustomerByIdAsync(request.CustomerId, cancellationToken);
        if (customerResponse.customer == null)
            return new CreateOrdersResponse { Success = false, Message = "Customer not found." };

        var customer = customerResponse.customer;

        var order = new Order
        {
            CustomerId = customer.Id,
            Customer = customer,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        foreach (var lineDto in request.Lines)
        {
            var (product, message) = await _productService.GetByIdAsync(lineDto.ProductId, cancellationToken);
            if (product == null) continue;

            var orderLine = new OrderLine
            {
                ProductId = product.Id,
                Product = product,
                Quantity = lineDto.Quantity,
                Total = product.Price * lineDto.Quantity,
                OrderId = order.Id
            };

            order.Lines.Add(orderLine);
        }

        await _orderRepository.AddAsync(order, cancellationToken);

        return new CreateOrdersResponse { Order = order, Success = true };
    }
}
