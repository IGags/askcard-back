using System;

namespace Logic.Managers.ConfirmOperation.Models;

public class ConfirmOperationCreateResultModel
{
    public Guid OperationId { get; set; }
    
    public string Code { get; set; }
}