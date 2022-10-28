using System.Runtime.InteropServices;
using System.Security;

namespace Bamboomaui.Extensions;

/// <summary>
/// 
/// </summary>
public static class SecureStringExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="secureString"></param>
    /// <returns></returns>
    public static string FromSecureString(this SecureString secureString)
    {
        IntPtr unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }
}