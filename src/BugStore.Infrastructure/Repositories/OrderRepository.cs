using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces;
using BugStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Order>> SearchAsync(
        string? customerName,
        string? customerEmail,
        string? customerPhone,
        string? productTitle,
        string? productDescription,
        string? productSlug,
        decimal? productPriceStart,
        decimal? productPriceEnd,
        DateTime? createdAtStart,
        DateTime? createdAtEnd,
        DateTime? updatedAtStart,
        DateTime? updatedAtEnd,
        CancellationToken cancellationToken)
    {
        var query = _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Lines)
                .ThenInclude(l => l.Product)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(customerName))
            query = query.Where(o => o.Customer.Name.ToLower().Contains(customerName.ToLower()));

        if (!string.IsNullOrWhiteSpace(customerEmail))
            query = query.Where(o => o.Customer.Email.ToLower().Contains(customerEmail.ToLower()));

        if (!string.IsNullOrWhiteSpace(customerPhone))
            query = query.Where(o => o.Customer.Phone.ToLower().Contains(customerPhone.ToLower()));

        if (!string.IsNullOrWhiteSpace(productTitle))
            query = query.Where(o => o.Lines.Any(l => l.Product.Title.ToLower().Contains(productTitle.ToLower())));

        if (!string.IsNullOrWhiteSpace(productDescription))
            query = query.Where(o => o.Lines.Any(l => l.Product.Description.ToLower().Contains(productDescription.ToLower())));

        if (!string.IsNullOrWhiteSpace(productSlug))
            query = query.Where(o => o.Lines.Any(l => l.Product.Slug.ToLower().Contains(productSlug.ToLower())));

        if (productPriceStart.HasValue)
            query = query.Where(o => o.Lines.Any(l => l.Product.Price >= productPriceStart));

        if (productPriceEnd.HasValue)
            query = query.Where(o => o.Lines.Any(l => l.Product.Price <= productPriceEnd));

        if (createdAtStart.HasValue)
            query = query.Where(o => o.CreatedAt >= createdAtStart.Value.Date);

        if (createdAtEnd.HasValue)
            query = query.Where(o => o.CreatedAt <= createdAtEnd.Value.AddDays(1).AddTicks(-1));

        if (updatedAtStart.HasValue)
            query = query.Where(o => o.UpdatedAt >= updatedAtStart.Value.Date);

        if (updatedAtEnd.HasValue)
            query = query.Where(o => o.UpdatedAt <= updatedAtEnd.Value.AddDays(1).AddTicks(-1));

        return await query.ToListAsync(cancellationToken);
    }


}
