using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface ITenantDbContext
{
    DbSet<Product> Products { get; set; }
    Task<bool> SaveAsync(CancellationToken cancellationToken = default);
}