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
    public class GetProductsHandlerTests
    {
        private FakeProductsService _fakeProductsService;
        private GetProductsHandler _handler;
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

            _mapperMock
            .Setup(m => m.Map<List<ProductDto>>(It.IsAny<List<Product>>()))
            .Returns((List<Product> pList) => pList.Select(p => new ProductDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Slug = p.Slug,
                Price = p.Price
            }).ToList());

            _handler = new GetProductsHandler(_fakeProductsService, _mapperMock.Object);
        }

        [TestMethod]
        [TestCategory("GetProductsHandler")]
        public async Task Dado_existirem_produtos_deve_retornar_todos_produtos()
        {
            // Arrange
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Produto 1 Desafio Balta",
                Description = "Descrição do Produto 1 desafio Balta",
                Slug = "produto1-desafio-balta",
                Price = 50m
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Produto 2 Desafio Balta",
                Description = "Descrição do Produto 2 desafio Balta",
                Slug = "produto2-desafio-balta",
                Price = 100m
            };

            await _fakeProductsService.AddProductAsync(product1);
            await _fakeProductsService.AddProductAsync(product2);

            var request = new GetProductsRequest();

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Products);
            Assert.AreEqual(2, response.Products?.Count);
            Assert.IsTrue(response.Products?.Any(p => p.Id == product1.Id));
            Assert.IsTrue(response.Products?.Any(p => p.Id == product2.Id));
        }

        [TestMethod]
        [TestCategory("GetProductsHandler")]
        public async Task Dado_nao_existirem_produtos_deve_retornar_lista_vazia()
        {
            // Arrange
            var request = new GetProductsRequest();

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Products);
            Assert.AreEqual(0, response.Products?.Count);
        }
    }
}
