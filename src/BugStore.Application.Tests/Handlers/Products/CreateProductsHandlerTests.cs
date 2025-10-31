using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Requests.Products;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Application.Tests.Fakes.Services;
using BugStore.Domain.Entities;
using Moq;

namespace BugStore.Application.Handlers.Products
{
    [TestClass]
    public class CreateProductsHandlerTests
    {
        private FakeProductsService _fakeProductsService;
        private CreateProductsHandler _handler;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void Setup()
        {
            var fakeRepo = new FakeProductsRepository();
            _fakeProductsService = new FakeProductsService(fakeRepo);

            _mapperMock = new Mock<IMapper>();
            _mapperMock
                .Setup(m => m.Map<ProductDto>(It.IsAny<Product>()))
                .Returns((Product p) => new ProductDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Slug = p.Slug,
                    Price = p.Price
                });

            _handler = new CreateProductsHandler(_fakeProductsService, _mapperMock.Object);
        }

        [TestMethod]
        [TestCategory("CreateProductsHandler")]
        public async Task Dado_um_request_valido_deve_criar_um_produto()
        {
            // Arrange
            var request = new CreateProductsRequest
            {
                Title = "Produto Desafio Balta",
                Description = "Descrição do Produto Teste desafio Balta",
                Slug = "produto-desafio-balta",
                Price = 99.99m
            };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Product);
            Assert.AreEqual(request.Title, response.Product.Title);
            Assert.AreEqual(request.Slug, response.Product.Slug);
        }
    }
}
