using System.Text;

namespace Bamboomaui.Extensions;

public static class ByteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string ByteToHex(this byte[] data)
    {
        return Convert.ToHexString(data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string ByteToHex(this ReadOnlySpan<byte> data)
    {
        return Convert.ToHexString(data);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string FromBytes(this byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }
}