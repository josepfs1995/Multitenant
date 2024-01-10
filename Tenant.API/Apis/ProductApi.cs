using Application.Products.CreateProduct;
using Application.Products.DeleteProduct;
using Application.Products.ListProducts;
using Application.Products.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Tenant.API.Apis;

public static class ProductApi
{
    public static IEndpointRouteBuilder MapProductApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", ListProductsAsync);
        app.MapPost("/", CreateProductAsync);
        app.MapPut("/", UpdateProductAsync);
        app.MapDelete("/{id}", DeleteProductAsync);
        return app;
    }
    private static async Task<Results<Ok<IList<ProductDto>>, StatusCodeHttpResult>> ListProductsAsync([FromServices] IMediator mediator)
    {
        try
        {
            return TypedResults.Ok(await mediator.Send(new ListProductQuery()));
        }
        catch (Exception)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    private static async Task<Results<Ok, StatusCodeHttpResult>> CreateProductAsync([FromServices] IMediator mediator, CrateProductCommand command)
    {
        try
        {
            await mediator.Send(command);
            return TypedResults.Ok();
        }
        catch (Exception)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    private static async Task<Results<Ok, BadRequest<string>, StatusCodeHttpResult>> UpdateProductAsync([FromServices] IMediator mediator, UpdateProductCommand command)
    {
        try
        {
            await mediator.Send(command);
            return TypedResults.Ok();
        }
        catch (ApplicationException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    private static async Task<Results<Ok, BadRequest<string>, StatusCodeHttpResult>> DeleteProductAsync([FromServices] IMediator mediator, Guid id)
    {
        try
        {
            await mediator.Send(new DeleteProductCommand(id));
            return TypedResults.Ok();
        }
        catch (ApplicationException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}