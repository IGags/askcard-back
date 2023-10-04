using System;
using Core.RepositoryBase.Model;
using Newtonsoft.Json.Linq;

namespace Dal.UserOperation;

/// <summary>
/// Модель операции, совершаемой пользователем
/// </summary>
public class UserOperationDal : DalModelBase<Guid>
{
    /// <summary>
    /// Имя операции
    /// </summary>
    public string OperationName { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Код подтверждения
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Дата истечения операции
    /// </summary>
    public DateTime ExpirationDate { get; set; }
    
    /// <summary>
    /// Попыток осталось
    /// </summary>
    public int LeftAttempts { get; set; }
    
    /// <summary>
    /// Кастомные данные
    /// </summary>
    public string CustomData { get; set; }
}