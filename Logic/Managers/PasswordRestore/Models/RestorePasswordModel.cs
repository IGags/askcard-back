using System;
using Core.DbCustomTypes;

namespace Logic.Managers.PasswordRestore.Models;

public class RestorePasswordModel
{
    public NormalizedString Email { get; set; }
    
    public string NewPassword { get; set; }
}