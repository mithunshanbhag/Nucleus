﻿using System.Security.Cryptography;
using System.Text;

namespace Nucleus.Misc.ExtensionMethods;

public static class ObjectExtensions
{
    public static int GetDeterministicHashCode(this object input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input.ToString()!));
        var hash = BitConverter.ToInt32(bytes, 0);
        return hash;
    }
}