using System;
using System.Linq;

namespace Core.Helpers;

public static class SecretCodeGenerationHelper
{
    private const string Chars = "0123456789";
    private static readonly Random Random = new();
    
    public static string GenerateCode(int length)
    {
        return new string(Enumerable.Repeat(Chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}