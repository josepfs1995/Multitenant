using MediatR;

namespace Application.Users.SignUp;

public record SignUpCommand(string Email, string Password, Guid OrganizationId) : IRequest;