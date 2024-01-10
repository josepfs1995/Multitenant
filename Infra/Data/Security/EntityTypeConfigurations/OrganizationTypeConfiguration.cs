using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Security.EntityTypeConfigurations;

public class OrganizationTypeConfiguration: IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnType("VARCHAR(150)");
        builder.Property(x => x.SlugTenant).HasColumnType("VARCHAR(150)");
    }
}