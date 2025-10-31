using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;

namespace BugStore.Application.Tests.Fakes.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customer = new();

        public Task AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            _customer.Add(customer);
            return Task.CompletedTask;
        }

        public Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_customer.ToList());
        }

        public Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return Task.FromResult(_customer.FirstOrDefault(p => p.Email == email));
        }

        public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult(_customer.FirstOrDefault(p => p.Id == id));
        }

        public Task RemoveAsync(Customer customer, CancellationToken cancellationToken)
        {
            return Task.FromResult(_customer.Remove(customer));
        }

        public Task<List<Customer>> SearchAsync(string? name, string? email, string? phone, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            var index = _customer.FindIndex(c => c.Id == customer.Id);

            if (index >= 0)
            {
                _customer[index] = customer;
            }

            return Task.CompletedTask;
        }

    }
}
