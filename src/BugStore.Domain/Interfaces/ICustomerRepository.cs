using BugStore.Domain.Entities;

namespace BugStore.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
        Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task AddAsync(Customer customer, CancellationToken cancellationToken);
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
        Task RemoveAsync(Customer customer, CancellationToken cancellationToken);
        Task<List<Customer>> SearchAsync(string? name, string? email, string? phone, CancellationToken cancellationToken);

    }
}
