using Core.Middleware.ExceptionHandling;

namespace Logic.Exceptions;

public class IncorrectCodeException : BaseErrorCodeException
{
    public IncorrectCodeException(int attemptsLeft) : base($"Неверный код подтверждения. Осталось попыток:{attemptsLeft}")
    {
        
    }
}