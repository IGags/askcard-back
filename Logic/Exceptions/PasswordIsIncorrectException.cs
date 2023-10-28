using Core.Middleware.ExceptionHandling;

namespace Logic.Exceptions;

public class PasswordIsIncorrectException : BaseErrorCodeException
{
    public PasswordIsIncorrectException() : base("Введён некорректный пароль")
    {
        
    }
}