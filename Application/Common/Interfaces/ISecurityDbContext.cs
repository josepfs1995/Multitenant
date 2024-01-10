using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface ISecurityDbContext
{
    DbSet<Organization> Organizations { get; set; }
    Task<bool> SaveAsync(CancellationToken cancellationToken = default);
}