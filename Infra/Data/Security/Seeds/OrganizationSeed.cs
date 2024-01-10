using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Security.Seeds;

public static class OrganizationSeed
{
    public static void AddOrganizationSeed(this ModelBuilder builder)
    {
        builder.Entity<Organization>().HasData(new Organization
        {
            Id = new Guid("ba3bef5e-5250-4f49-8719-ebdb0bf2b58f"),
            Name = "One",
            SlugTenant = "One"
        }, new Organization
        {
            Id = new Guid("7e6f1a79-c36c-410e-a478-306bf0adb053"),
            Name = "Two",
            SlugTenant = "Two"
        });
    }
}