using System;

namespace Logic.Managers.ConfirmOperation.Models;

public class ConfirmOperationModel<TData> where TData : class
{
    public Guid UserId { get; set; }
    
    public TData CustomData { get; set; }
    
    public int AttemptCount { get; set; }
    
    public string Code { get; set; }
}