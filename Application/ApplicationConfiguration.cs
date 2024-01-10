using Application.Users.Login;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly);
        });
        
        return services;
    }
}