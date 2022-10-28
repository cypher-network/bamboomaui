using System.Runtime.InteropServices;
using System.Security;

namespace Bamboomaui.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SecureString ToSecureString(this string value)
    {
        var secureString = new SecureString();
        Array.ForEach(value.ToArray(), secureString.AppendChar);
        secureString.MakeReadOnly();
        return secureString;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static byte[] HexToByte(this string hex)
    {
        return Convert.FromHexString(hex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hex"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static byte[] HexToByte<T>(this T hex)
    {
        return Convert.FromHexString(hex.ToString()!);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static void ZeroString(this string value)
    {
        var handle = GCHandle.Alloc(value, GCHandleType.Pinned);
        unsafe
        {
            var pValue = (char*)handle.AddrOfPinnedObject();
            for (int index = 0; index < value.Length; index++)
            {
                pValue[index] = char.MinValue;
            }
        }

        handle.Free();
    }
}