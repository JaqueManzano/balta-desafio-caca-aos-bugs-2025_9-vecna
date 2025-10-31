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
    public class GetByIdProductHandlerTests
    {
        private FakeProductsService _fakeProductsService;
        private GetByIdProductHandler _handler;
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


            _handler = new GetByIdProductHandler(_fakeProductsService, _mapperMock.Object);
        }

        [TestMethod]
        [TestCategory("GetByIdProductHandler")]
        public async Task Dado_um_produto_existente_deve_retornar_o_produto()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Produto Desafio Balta",
                Description = "Descrição do Produto Teste desafio Balta",
                Slug = "produto-desafio-balta",
                Price = 99.99m
            };

            await _fakeProductsService.AddProductAsync(product);

            var request = new GetByIdProductsRequest
            {
                Id = product.Id
            };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Product);
        }

        [TestMethod]
        [TestCategory("GetByIdProductHandler")]
        public async Task Dado_um_produto_inexistente_deve_retornar_nulo()
        {
            // Arrange
            var request = new GetByIdProductsRequest
            {
                Id = Guid.NewGuid() 
            };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response.Product);
        }
    }
}
