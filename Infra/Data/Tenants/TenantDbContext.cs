using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Tenants;

public class TenantDbContext: DbContext, ITenantDbContext
{
    private readonly ITenantService _tenantService;
    private readonly IConfiguration _configuration;

    public TenantDbContext(ITenantService tenantService, IConfiguration configuration)
    {
        _tenantService = tenantService;
        _configuration = configuration;
    }
    
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenant = _tenantService.GetTenant();
        var connectionString = _configuration.GetConnectionString(tenant);
        optionsBuilder.UseSqlServer(connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        var response = await base.SaveChangesAsync(cancellationToken);
        return response > 0;
    }
}