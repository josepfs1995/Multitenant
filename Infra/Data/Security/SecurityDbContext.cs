using Application.Common.Interfaces;
using Domain.Models;
using Infra.Data.Security.EntityTypeConfigurations;
using Infra.Data.Security.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Security;

public class SecurityDbContext : IdentityDbContext<ApplicationUser>, ISecurityDbContext
{
    public DbSet<Organization> Organizations { get; set; }
    
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.AddOrganizationSeed();
        builder.ApplyConfiguration(new OrganizationTypeConfiguration());
    }
    
    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        var response = await base.SaveChangesAsync(cancellationToken);
        return response > 0;
    }
}