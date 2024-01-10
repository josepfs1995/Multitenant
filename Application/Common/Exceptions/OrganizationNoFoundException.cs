namespace Application.Common.Exceptions;

public class OrganizationNoFoundException : Exception
{
    public OrganizationNoFoundException()
    {
    }

    public OrganizationNoFoundException(string message) : base(message)
    {
    }

    public OrganizationNoFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}
