using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Requests.Customers;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Application.Tests.Fakes.Services;
using BugStore.Domain.Entities;
using BugStore.Handlers.Customers;
using Moq;

namespace BugStore.Application.Handlers.Customers;

[TestClass]
public class GetCustomerHandlerTests
{
    private FakeCustomerService _fakeCustomerService = null!;
    private GetCustomerHandler _handler = null!;
    private Mock<IMapper> _mapperMock;

    [TestInitialize]
    public void Setup()
    {
        var fakeRepo = new FakeCustomerRepository();
        _fakeCustomerService = new FakeCustomerService(fakeRepo);

        _mapperMock = new Mock<IMapper>();
        _mapperMock
            .Setup(m => m.Map<CustomerDto>(It.IsAny<Customer>()))
            .Returns((Customer c) => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                BirthDate = c.BirthDate
            });

        _mapperMock
        .Setup(m => m.Map<List<CustomerDto>>(It.IsAny<List<Customer>>()))
        .Returns((List<Customer> list) =>
        list == null ? new List<CustomerDto>() : list.Select(c => new CustomerDto { Id = c.Id, Name = c.Name }).ToList());

        _handler = new GetCustomerHandler(_fakeCustomerService, _mapperMock.Object);
    }

    [TestMethod]
    [TestCategory("GetCustomerHandler")]
    public async Task Dado_clientes_existentes_deve_retornar_uma_lista()
    {
        // Arrange
        var customer1 = new Customer
        {
            Id = Guid.NewGuid(),
            Name = "Jaqueline",
            Email = "jaqueline@example.com"
        };
        var customer2 = new Customer
        {
            Id = Guid.NewGuid(),
            Name = "Denise",
            Email = "denise@example.com"
        };

        await _fakeCustomerService.AddCustomerAsync(customer1);
        await _fakeCustomerService.AddCustomerAsync(customer2);

        var request = new GetCustomerRequest();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(response.Success);
        Assert.AreEqual(2, response.Customers?.Count);
    }

    [TestMethod]
    [TestCategory("GetCustomerHandler")]
    public async Task Dado_lista_vazia_deve_retornar_sucesso_com_lista_vazia()
    {
        // Arrange
        var request = new GetCustomerRequest();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(response.Success);
        Assert.AreEqual(0, response.Customers.Count);
    }
}
