using BugStore.Application.Requests.Customers;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Application.Tests.Fakes.Services;
using BugStore.Domain.Entities;

namespace BugStore.Application.Handlers.Customers;

[TestClass]
public class DeleteCustomerHandlerTests
{
    private FakeCustomerService _fakeCustomerService = null!;
    private DeleteCustomerHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        var fakeRepo = new FakeCustomerRepository();
        _fakeCustomerService = new FakeCustomerService(fakeRepo);
        _handler = new DeleteCustomerHandler(_fakeCustomerService);
    }

    [TestMethod]
    [TestCategory("DeleteCustomerHandler")]
    public async Task Dado_um_cliente_existente_deve_deletar_com_sucesso()
    {
        // Arrange
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = "Jaqueline",
            Email = "jaqueline@example.com"
        };
        await _fakeCustomerService.AddCustomerAsync(customer);

        var request = new DeleteCustomerRequest { Id = customer.Id };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        var (deletedCustomer, _) = await _fakeCustomerService.GetCustomerByIdAsync(customer.Id, CancellationToken.None);
        Assert.IsNull(deletedCustomer);
    }

    [TestMethod]
    [TestCategory("DeleteCustomerHandler")]
    public async Task Dado_um_cliente_inexistente_nao_deve_retornar_sucesso()
    {
        // Arrange
        var request = new DeleteCustomerRequest { Id = Guid.NewGuid() };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsFalse(response.Success);
    }
}
