using Core.Middleware.ExceptionHandling;

namespace Logic.Exceptions;

public class OperationIsExpiredException : BaseErrorCodeException
{
    public OperationIsExpiredException() : base("Срок действия операции истёк")
    {
        
    }
}