using BugStore.Application.Interfaces;
using BugStore.Application.Tests.Fakes.Repositories;
using BugStore.Domain.Entities;

namespace BugStore.Application.Tests.Fakes.Services
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly FakeCustomerRepository _repo;

        public FakeCustomerService(FakeCustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<(Customer? customer, string message)> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _repo.GetByIdAsync(id, cancellationToken);
            return (customer, customer == null ? "Not found" : "Found");
        }

        public async Task<(List<Customer> customers, bool success, string message)> GetAllCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _repo.GetAllAsync(cancellationToken);
            return (customers, true, "Customers retrieved");
        }

        public async Task<(Customer? customer, bool success, string message)> CreateCustomerAsync(
            string name, string email, string phone, DateTime birthDate, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Phone = phone,
                BirthDate = birthDate
            };
            await _repo.AddAsync(customer, cancellationToken);
            return (customer, true, "Customer created");
        }

        public async Task<(Customer? customer, bool success, string message)> UpdateCustomerAsync(
            Guid id, string? name, string? email, string? phone, DateTime? birthDate, CancellationToken cancellationToken)
        {
            var customer = await _repo.GetByIdAsync(id, cancellationToken);
            if (customer == null)
                return (null, false, "Customer not found");

            if (!string.IsNullOrEmpty(name)) customer.Name = name;
            if (!string.IsNullOrEmpty(email)) customer.Email = email;
            if (!string.IsNullOrEmpty(phone)) customer.Phone = phone;
            if (birthDate.HasValue) customer.BirthDate = birthDate.Value;

            return (customer, true, "Customer updated");
        }

        public async Task<(bool success, string message)> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _repo.GetByIdAsync(id, cancellationToken);
            if (customer == null)
                return (false, "Customer not found.");

            await _repo.RemoveAsync(customer, cancellationToken);
            return (true, "Customer deleted.");
        }

        public Task AddCustomerAsync(Customer customer)
        {
            return _repo.AddAsync(customer, CancellationToken.None);
        }
    }
}
