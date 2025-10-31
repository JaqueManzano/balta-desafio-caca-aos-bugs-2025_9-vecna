using BugStore.Application.Requests.Orders;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Application.Tests.Fakes.Services;

namespace BugStore.Application.Handlers.Order
{
    [TestClass]
    public class CreateOrdersHandlerTests
    {
        private FakeOrderService _fakeOrderService;
        private CreateOrdersHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var fakeRepo = new FakeOrderRepository();
            _fakeOrderService = new FakeOrderService(fakeRepo);

            _handler = new CreateOrdersHandler(_fakeOrderService);
        }

        [TestMethod]
        [TestCategory("CreateOrdersHandler")]
        public async Task Dado_um_request_valido_deve_criar_um_pedido_com_itens()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId1 = Guid.NewGuid();
            var productId2 = Guid.NewGuid();

            var request = new CreateOrdersRequest
            {
                CustomerId = customerId,
                Lines = new List<CreateOrderLineRequestDto>
                {
                    new CreateOrderLineRequestDto { ProductId = productId1, Quantity = 2 },
                    new CreateOrderLineRequestDto { ProductId = productId2, Quantity = 1 }
                }
            };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Order);
            Assert.AreEqual(customerId, response.Order.CustomerId);
            Assert.IsNotNull(response.Order.Lines);
            Assert.AreEqual(2, response.Order.Lines.Count);

            Assert.AreEqual(productId1, response.Order.Lines[0].ProductId);
            Assert.AreEqual(2, response.Order.Lines[0].Quantity);

            Assert.AreEqual(productId2, response.Order.Lines[1].ProductId);
            Assert.AreEqual(1, response.Order.Lines[1].Quantity);

            Assert.IsTrue(response.Success);
        }
    }
}
