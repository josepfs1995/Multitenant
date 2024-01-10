using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class ApplicationUser: IdentityUser
{
    public ApplicationUser()
    {
        
    }
    public ApplicationUser(string userName): base(userName)
    {
        
    }
    public Guid? OrganizationId { get; set; }
}