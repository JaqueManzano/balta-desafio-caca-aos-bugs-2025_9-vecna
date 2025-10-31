using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Application.Requests.Customers;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Application.Tests.Fakes.Services;
using BugStore.Domain.Entities;
using Moq;

namespace BugStore.Application.Handlers.Customers;

[TestClass]
public class CreateCustomerHandlerTests
{
    private FakeCustomerService _fakeCustomerService;
    private CreateCustomerHandler _handler;
    private CreateCustomerRequest _baseCustomerRequest;
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

        _baseCustomerRequest = new CreateCustomerRequest
        {
            Name = "Jaqueline Manzano",
            Email = "jaqueline@example.com",
            Phone = "123456789",
            BirthDate = new DateTime(1900, 1, 1)
        };

        _handler = new CreateCustomerHandler(_fakeCustomerService, _mapperMock.Object);
    }

    [TestMethod]
    [TestCategory("CreateCustomerHandler")]
    public async Task Dado_um_request_valido_deve_criar_um_cliente()
    {
        // Act
        var response = await _handler.Handle(_baseCustomerRequest, CancellationToken.None);

        // Assert
        Assert.IsNotNull(response);
    }
}