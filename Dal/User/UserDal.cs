using System;
using Core.RepositoryBase.Model;

namespace Dal.User;

/// <summary>
/// Пользоватль
/// </summary>
public class UserDal : DalModelBase<Guid>
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; set; }
}