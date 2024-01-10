using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.UpdateProduct;

public class UpdateProductCommandHandler(ITenantDbContext tenantDbContext) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await tenantDbContext.Products.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null)
        {
            throw new ApplicationException("No existe un producto con ese Id");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Quantity = request.Quantity;

        tenantDbContext.Products.Update(product);
        await tenantDbContext.SaveAsync(cancellationToken);
    }
}