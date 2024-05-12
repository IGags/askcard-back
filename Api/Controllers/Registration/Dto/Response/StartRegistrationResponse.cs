using System;

namespace Api.Controllers.Registration.Dto.Response;

/// <summary>
/// Ответ на старт регистрации
/// </summary>
public class StartRegistrationResponse
{
    /// <summary>
    /// Идентификатор операции
    /// </summary>
    public string OperationId { get; set; }
}