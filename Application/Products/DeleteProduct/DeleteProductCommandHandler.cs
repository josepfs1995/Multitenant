using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.DeleteProduct;

public class DeleteProductCommandHandler(ITenantDbContext tenantDbContext): IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await tenantDbContext.Products.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null)
        {
            throw new ApplicationException("No existe un producto con ese Id");
        }
        
        tenantDbContext.Products.Remove(product);
        await tenantDbContext.SaveAsync(cancellationToken);
    }
}