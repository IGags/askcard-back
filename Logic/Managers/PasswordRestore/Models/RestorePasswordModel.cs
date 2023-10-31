using System;

namespace Logic.Managers.PasswordRestore.Models;

public class RestorePasswordModel
{
    public Guid UserId { get; set; }
    
    public string NewPassword { get; set; }
    
    public string Code { get; set; }
}