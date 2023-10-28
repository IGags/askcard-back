using Core.Middleware.ExceptionHandling;

namespace Logic.Exceptions;

public class UserDoesNotExistsException : BaseErrorCodeException
{
    public UserDoesNotExistsException() : base("Пользователь не существует")
    {
        
    }
}