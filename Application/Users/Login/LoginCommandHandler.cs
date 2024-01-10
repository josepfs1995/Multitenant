using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Login;

public class LoginCommandHandler(
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    SignInManager<ApplicationUser> signInManager,
    ISecurityDbContext securityDbContext)
    : IRequestHandler<LoginCommand, LoginDto>
{
    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var identityUser = await userManager.FindByEmailAsync(request.Email);
        if(identityUser == null) throw new UserOrPasswordInvalidException();
        var response = await signInManager.PasswordSignInAsync(user: identityUser, password: request.Password, isPersistent: true, lockoutOnFailure: false);
        if(!response.Succeeded) throw new UserOrPasswordInvalidException();

        var organization = securityDbContext.Organizations.Single(x => x.Id == identityUser.OrganizationId);
        var accessToken = tokenService.GenerateToken(identityUser);
        return new LoginDto(accessToken, organization.SlugTenant);
    }
}