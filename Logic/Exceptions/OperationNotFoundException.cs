using System;
using Core.Middleware.ExceptionHandling;

namespace Logic.Exceptions;

public class OperationNotFoundException : BaseErrorCodeException
{
    public OperationNotFoundException(string operationId) 
        : base($"Операция с идетификатором {operationId} не найдена")
    {
        
    }
}