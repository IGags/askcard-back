using System;
using Core.RepositoryBase.Model;

namespace Dal.Attempt.Models;

/// <summary>
/// Попытка
/// </summary>
public class AttemptDal : DalModelBase<Guid>
{
    public int UserScore { get; set; }
    
    public int MaxPossibleScore { get; set; }
    
    public TimeSpan AttemptTime { get; set; }
    
    public DateTime AttemptStartTime { get; set; }
    
    public Guid UserId { get; set; }
}