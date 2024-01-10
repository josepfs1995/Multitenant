using MediatR;

namespace Application.Products.ListProducts;

public record ListProductQuery(): IRequest<IList<ProductDto>>;