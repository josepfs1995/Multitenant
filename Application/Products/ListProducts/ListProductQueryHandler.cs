using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.ListProducts;

public class ListProductQueryHandler(ITenantDbContext tenantDbContext)
    : IRequestHandler<ListProductQuery, IList<ProductDto>>
{
    public async Task<IList<ProductDto>> Handle(ListProductQuery request, CancellationToken cancellationToken)
    {
        var products = await tenantDbContext.Products.AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);
        return products.Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Quantity)).ToList();
    }
}