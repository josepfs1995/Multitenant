using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Products.CreateProduct;

public class CrateProductCommandHandler(ITenantDbContext tenantDbContext) : IRequestHandler<CrateProductCommand>
{
    public async Task Handle(CrateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Quantity = request.Quantity
        };

        tenantDbContext.Products.Add(product);
        await tenantDbContext.SaveAsync(cancellationToken);
    }
}