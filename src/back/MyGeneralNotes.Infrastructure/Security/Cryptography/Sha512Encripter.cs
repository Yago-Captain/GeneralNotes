﻿using MyGeneralNotes.Domain.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace MyGeneralNotes.Infrastructure.Security.Cryptography;
public class Sha512Encripter(string additionalKey) : IPasswordEncripter
{
    private readonly string _additionalKey = additionalKey;

    public string Encrypt(string password)
    {
        var newPassword = $"{password}{_additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
