using MediatR;

namespace Application.Products.CreateProduct;

public record CrateProductCommand(string Name, string Description, int Quantity) : IRequest;
