using Domain.Models;

namespace Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser applicationUser);
}