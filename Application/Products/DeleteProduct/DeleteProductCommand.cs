using MediatR;

namespace Application.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id): IRequest;