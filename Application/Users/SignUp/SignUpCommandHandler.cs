using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.SignUp;

public class SignUpCommandHandler(UserManager<ApplicationUser> userManager, ISecurityDbContext securityDbContext)
    : IRequestHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        //Ignoro validaciones de modelo por tiempo. Utilizaria FluentValidation
        var organization = await securityDbContext.Organizations.SingleOrDefaultAsync(x => x.Id == request.OrganizationId, cancellationToken);
        if (organization is null)
        {
            throw new OrganizationNoFoundException();
        }
        
        var identityUser = new ApplicationUser(request.Email);
        identityUser.Email = request.Email;
        identityUser.EmailConfirmed = true;
        identityUser.OrganizationId = request.OrganizationId;
        identityUser.PhoneNumberConfirmed = true;
        var response = await userManager.CreateAsync(identityUser, request.Password);

        if (!response.Succeeded)
        {
            throw new ApplicationException(string.Join(',', response.Errors.Select(x => x.Description)));
        }
    }
}