using BugStore.Domain.Entities;
using BugStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Repositories;

[TestClass]
public class CustomerRepositoryTests
{
    private CustomerRepository _repository;
    private AppDbContext _context;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        _context = new AppDbContext(options);
        _repository = new CustomerRepository(_context);
    }

    [TestMethod]
    public async Task Deve_inserir_e_recuperar_cliente()
    {
        var customer = new Customer { Name = "Teste", Email = "email@teste.com", Phone = "14997060490" };
        await _repository.AddAsync(customer, CancellationToken.None);

        var result = await _repository.GetByIdAsync(customer.Id, CancellationToken.None);

        Assert.IsNotNull(result);
    }
}
