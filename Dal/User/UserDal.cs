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
    public required string Name { get; set; }
    
    /// <summary>
    /// Хэш пароля
    /// </summary>
    public required string PasswordHash { get; set; }
    
    /// <summary>
    /// Мыло пользователя
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Согласен ли пользователь на рассылку
    /// </summary>
    public required bool IsAgree { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public required string Role { get; set; }
}