using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers;

public static class PasswordHashHelper
{
    private static readonly byte[] Key = "5834196507"u8.ToArray();
    
    public static string GetPasswordHash(string password)
    {
        using var sha = new HMACSHA256(Key);
        var result = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(result);
    }
}