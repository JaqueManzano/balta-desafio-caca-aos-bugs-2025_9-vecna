using BugStore.Application.Requests.Orders;
using BugStore.Application.Services;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Application.Tests.Fakes.Services;
using BugStore.Domain.Entities;


namespace BugStore.Test.Unit.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private FakeOrderRepository _fakeOrderRepository;
        private FakeCustomerService _fakeCustomerService;
        private FakeProductsService _fakeProductsService;
        private OrderService _service;

        [TestInitialize]
        public void Setup()
        {
            _fakeOrderRepository = new FakeOrderRepository();
            _fakeCustomerService = new FakeCustomerService(new FakeCustomerRepository());
            _fakeProductsService = new FakeProductsService(new FakeProductsRepository());

            _service = new OrderService(_fakeCustomerService, _fakeProductsService, _fakeOrderRepository);
        }

        [TestMethod]
        [TestCategory("OrderService")]
        public async Task Dado_um_order_existente_deve_retornar_order()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };
            await _fakeOrderRepository.AddAsync(order, CancellationToken.None);

            var request = new GetByIdOrdersRequest { Id = order.Id };

            // Act
            var response = await _service.GetOrderByIdAsync(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Order);
        }

        [TestMethod]
        [TestCategory("OrderService")]
        public async Task Dado_um_order_inexistente_deve_retornar_null()
        {
            // Arrange
            var request = new GetByIdOrdersRequest { Id = Guid.NewGuid() };

            // Act
            var response = await _service.GetOrderByIdAsync(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response.Order);
        }

        [TestMethod]
        [TestCategory("OrderService")]
        public async Task Dado_insercao_de_novo_order_deve_criar_order()
        {
            // Arrange
            var customer = new Customer { Id = Guid.NewGuid(), Name = "Jaqueline Manzano" };
            await _fakeCustomerService.AddCustomerAsync(customer);

            var product = new Product { Id = Guid.NewGuid(), Price = 10m };
            await _fakeProductsService.AddProductAsync(product);

            var request = new CreateOrdersRequest
            {
                CustomerId = customer.Id,
                Lines = new List<CreateOrderLineRequestDto>
                {
                    new CreateOrderLineRequestDto
                    {
                        ProductId = product.Id,
                        Quantity = 2
                    }
                }
            };


            // Act
            var response = await _service.CreateOrderAsync(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Order);
        }
    }
}
