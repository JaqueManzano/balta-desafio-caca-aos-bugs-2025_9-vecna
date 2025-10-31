using BugStore.Application.Services;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Domain.Entities;

namespace BugStore.Test.Unit.Services;

[TestClass]
public class CustomerServiceTests
{
    private FakeCustomerRepository _fakeRepository;
    private CustomerService _service;
    private Customer _baseCustomer;

    [TestInitialize]
    public void Setup()
    {
        _fakeRepository = new FakeCustomerRepository(); 
        _service = new CustomerService(_fakeRepository);

        _baseCustomer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = "Jaqueline Manzano",
            Email = "jaqueline_manzano@example.com",
            Phone = "123456789",
            BirthDate = new DateTime(1900, 1, 1)
        };
    }

    [TestMethod]
    [TestCategory("CustomerService")]
    public async Task Dado_um_cliente_existente_deve_retornar_um_registro()
    {
        await _fakeRepository.AddAsync(_baseCustomer, CancellationToken.None);

        // Act
        var (resultCustomer, message) = await _service.GetCustomerByIdAsync(_baseCustomer.Id, CancellationToken.None);

        // Assert
        Assert.IsNotNull(resultCustomer);
        Assert.AreEqual(_baseCustomer.Id, resultCustomer!.Id);
    }

    [TestMethod]
    public async Task Dado_insercao_de_cliente_novo_deve_criar_cliente()
    {
        // Arrange
        var name = "Jaqueline Manzano";
        var email = "jaqueline_manzano@example.com";
        var phone = "123456789";
        var birthDate = new DateTime(1900, 1, 1);

        // Act
        var (createdCustomer, success, message) = await _service.CreateCustomerAsync(name, email, phone, birthDate, CancellationToken.None);

        // Assert
        Assert.IsTrue(success);
        Assert.IsNotNull(createdCustomer);
    }

    [TestMethod]
    [TestCategory("CustomerService")]
    public async Task Dado_um_cliente_inexistente_deve_retornar_null()
    {
        // Act
        var (resultCustomer, message) = await _service.GetCustomerByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsNull(resultCustomer);
    }

    [TestMethod]
    [TestCategory("CustomerService")]
    public async Task Dado_criacao_com_email_existente_deve_retornar_falha()
    {
        await _fakeRepository.AddAsync(_baseCustomer, CancellationToken.None);

        var (createdCustomer, success, message) = await _service.CreateCustomerAsync(
            "Outro Nome",
            "jaqueline_manzano@example.com", 
            "987654321",
            new DateTime(1990, 01, 01),
            CancellationToken.None
        );

        // Assert
        Assert.IsFalse(success);
        Assert.IsNotNull(message); 
    }

}
