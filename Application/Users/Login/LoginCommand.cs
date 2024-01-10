using MediatR;

namespace Application.Users.Login;

public record LoginCommand(string Email, string Password): IRequest<LoginDto>;
