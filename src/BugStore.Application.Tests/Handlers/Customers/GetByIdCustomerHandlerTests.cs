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
public class GetByIdCustomerHandlerTests
{
    private FakeCustomerService _fakeCustomerService;
    private GetByIdCustomerHandler _handler;
    private Customer _baseCustomer;
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

        _baseCustomer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = "Jaqueline Manzano",
            Email = "jaqueline_manzano@example.com",
            Phone = "123456789",
            BirthDate = new DateTime(1900, 1, 1)
        };

        _handler = new GetByIdCustomerHandler(_fakeCustomerService, _mapperMock.Object);
    }

    [TestMethod]
    [TestCategory("GetByIdCustomerHandler")]
    public async Task Dado_um_cliente_existente_deve_retornar_o_cliente()
    {
        // Arrange
        await _fakeCustomerService.AddCustomerAsync(_baseCustomer);

        var request = new GetByIdCustomerRequest { Id = _baseCustomer.Id };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(response.Customer);
        Assert.AreEqual(_baseCustomer.Id, response.Customer!.Id);
    }

    [TestMethod]
    [TestCategory("GetByIdCustomerHandler")]
    public async Task Dado_um_cliente_inexistente_deve_retornar_customer_null()
    {
        // Arrange
        var request = new GetByIdCustomerRequest { Id = Guid.NewGuid() };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNull(response.Customer);
    }
}