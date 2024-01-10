using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Tenants.EntityTypeConfigurations;

public class ProductTypeConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnType("VARCHAR(150)");
        builder.Property(x => x.Description).HasColumnType("VARCHAR(250)");
    }
}