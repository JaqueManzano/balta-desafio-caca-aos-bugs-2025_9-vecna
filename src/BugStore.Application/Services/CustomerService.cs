using BugStore.Application.Interfaces;
using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;

namespace BugStore.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<(Customer? customer, string message)> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return (null, "Invalid customer ID.");

            var customer = await _repository.GetByIdAsync(id, cancellationToken);

            if (customer == null)
                return (null, $"Customer with ID {id} not found.");

            return (customer, $"Customer with ID {id} found.");
        }

        public async Task<(List<Customer> customers, bool success, string message)> GetAllCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAllAsync(cancellationToken);

            if (customers == null || customers.Count == 0)
                return (new List<Customer>(), true, "No registered customers.");

            return (customers, true, string.Empty);
        }

        public async Task<(Customer? customer, bool success, string message)> CreateCustomerAsync(
            string name, string email, string phone, DateTime birthDate, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByEmailAsync(email, cancellationToken);
            if (exists != null)
                return (exists, false, "Customer already registered.");

            var customer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone,
                BirthDate = birthDate
            };

            await _repository.AddAsync(customer, cancellationToken);
            return (customer, true, "Customer created successfully.");
        }

        public async Task<(Customer? customer, bool success, string message)> UpdateCustomerAsync(
            Guid id,
            string? name,
            string? email,
            string? phone,
            DateTime? birthDate,
            CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return (null, false, "Invalid customer ID.");

            var customer = await _repository.GetByIdAsync(id, cancellationToken);

            if (customer == null)
                return (null, false, "Customer not found.");

            if (!string.IsNullOrWhiteSpace(name))
                customer.Name = name;

            if (!string.IsNullOrWhiteSpace(email))
                customer.Email = email;

            if (!string.IsNullOrWhiteSpace(phone))
                customer.Phone = phone;

            if (birthDate.HasValue)
                customer.BirthDate = birthDate.Value;

            await _repository.UpdateAsync(customer, cancellationToken);

            return (customer, true, "Customer successfully updated.");
        }

        public async Task<(bool success, string message)> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return (false, "Invalid customer ID.");

            var customer = await _repository.GetByIdAsync(id, cancellationToken);

            if (customer == null)
                return (false, $"Customer with ID {id} not found.");

            await _repository.RemoveAsync(customer, cancellationToken);

            return (true, $"Customer with ID {id} successfully deleted.");
        }


    }
}
