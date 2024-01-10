namespace Domain.Models;

public class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SlugTenant { get; set; }
}