using System;

namespace Logic.Managers.ConfirmOperation.Models;

public class ConfirmOperationModel<TData> where TData : class
{
    public string OperationName { get; set; }
    
    public Guid UserId { get; set; }
    
    public TData CustomData { get; set; }
    
    public TimeSpan? OperationLifetime { get; set; }
    
    public int? AttemptCount { get; set; }
    
    public int? CodeLength { get; set; }
}