using Application.Common.Exceptions;
using Application.Users.Login;
using Application.Users.SignUp;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Tenant.API.Apis;

public static class AuthApi
{
    public static IEndpointRouteBuilder MapAuthApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", LoginAsync);
        app.MapPost("/sign-up", SingUpAsync);
        return app;
    }
    
    private static async Task<Results<Ok<LoginDto>, BadRequest<string>, StatusCodeHttpResult>> LoginAsync([FromServices] IMediator mediator, LoginCommand command)
    {
        try
        {
            return TypedResults.Ok(await mediator.Send(command));
        }
        catch (UserOrPasswordInvalidException)
        {
            return TypedResults.BadRequest<string>("Usuario o contraseña incorrecto");
        }
        catch (Exception)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    private static async Task<Results<Ok, BadRequest<string>, Conflict<string>, StatusCodeHttpResult>> SingUpAsync([FromServices] IMediator mediator, SignUpCommand command)
    {
        try
        {
            await mediator.Send(command);
            return TypedResults.Ok();
        }
        catch (OrganizationNoFoundException)
        {
            return TypedResults.BadRequest<string>("No existe la organización");
        }
        catch (ApplicationException application)
        {
            return TypedResults.Conflict<string>(application.Message);
        }
        catch (Exception)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}