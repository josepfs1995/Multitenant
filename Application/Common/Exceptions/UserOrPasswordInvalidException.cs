namespace Application.Common.Exceptions;

public class UserOrPasswordInvalidException : Exception
{
    public UserOrPasswordInvalidException()
    {
    }

    public UserOrPasswordInvalidException(string message) : base(message)
    {
    }

    public UserOrPasswordInvalidException(string message, Exception inner) : base(message, inner)
    {
    }
}