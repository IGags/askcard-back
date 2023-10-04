using Core.Middleware.ExceptionHandling;

namespace Logic.Exceptions;

public class UserAlreadyExistsException : BaseErrorCodeException
{
    public UserAlreadyExistsException() : base("Пользователь с такой почтой уже существует")
    {
        
    }
}