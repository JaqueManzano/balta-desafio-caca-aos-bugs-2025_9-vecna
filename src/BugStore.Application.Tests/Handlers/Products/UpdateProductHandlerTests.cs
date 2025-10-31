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
    public class UpdateProductHandlerTests
    {
        private FakeProductsService _fakeProductsService;
        private UpdateProductHandler _handler;
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

            _handler = new UpdateProductHandler(_fakeProductsService, _mapperMock.Object);
        }

        [TestMethod]
        [TestCategory("UpdateProductHandler")]
        public async Task Dado_um_produto_existente_deve_atualizar_o_produto()
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

            var request = new UpdateProductsRequest
            {
                Id = product.Id,
                Slug = "produto-desafio-balta-atualizado",
                Price = 150m
            };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response.Product);
            Assert.AreEqual(response.Product.Slug, request.Slug);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        [TestCategory("UpdateProductHandler")]
        public async Task Dado_um_produto_inexistente_deve_retornar_sem_sucesso()
        {
            // Arrange
            var request = new UpdateProductsRequest
            {
                Id = Guid.NewGuid(),
                Slug = "produto-inexistente",
                Price = 200m
            };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNull(response.Product);
            Assert.IsFalse(response.Success);
        }
    }
}
