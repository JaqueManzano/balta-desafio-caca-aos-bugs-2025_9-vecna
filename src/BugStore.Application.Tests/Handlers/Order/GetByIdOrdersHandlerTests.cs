using BugStore.Application.Requests.Orders;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Application.Tests.Fakes.Services;

namespace BugStore.Application.Handlers.Order
{
    [TestClass]
    public class GetByIdOrdersHandlerTests
    {
        private FakeOrderRepository _fakeOrderRepository;
        private FakeOrderService _fakeOrderService;
        private GetByIdOrdersHandler _handler;
        private Guid _existingOrderId;

        [TestInitialize]
        public void Setup()
        {
            _fakeOrderRepository = new FakeOrderRepository();
            _fakeOrderService = new FakeOrderService(_fakeOrderRepository);

            _existingOrderId = Guid.NewGuid();
            Domain.Entities.Order? fakeOrder = new Domain.Entities.Order
            {
                Id = _existingOrderId,
                CustomerId = Guid.NewGuid(),
                Lines = new List<Domain.Entities.OrderLine>
                {
                    new Domain.Entities.OrderLine { ProductId = Guid.NewGuid(), Quantity = 2 },
                    new Domain.Entities.OrderLine { ProductId = Guid.NewGuid(), Quantity = 1 }
                }
            };

            _fakeOrderRepository.AddAsync(fakeOrder, CancellationToken.None).Wait();

            _handler = new GetByIdOrdersHandler(_fakeOrderService);
        }

        [TestMethod]
        [TestCategory("GetByIdOrdersHandler")]
        public async Task Dado_um_pedido_existente_deve_retornar_o_pedido()
        {
            // Arrange
            var request = new GetByIdOrdersRequest { Id = _existingOrderId };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response?.Order);
        }

        [TestMethod]
        [TestCategory("GetByIdOrdersHandler")]
        public async Task Dado_um_pedido_inexistente_deve_retornar_null()
        {
            // Arrange
            var request = new GetByIdOrdersRequest { Id = Guid.NewGuid() };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response.Order);
        }
    }
}
