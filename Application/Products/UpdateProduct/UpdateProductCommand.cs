using MediatR;

namespace Application.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name, string Description, int Quantity) : IRequest;
