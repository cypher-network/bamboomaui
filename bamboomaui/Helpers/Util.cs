using System.Security;
using Bamboomaui.Extensions;
using LiteDB;

namespace Bamboomaui.Helpers;

public static class Util
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="walletName"></param>
    /// <param name="passphrase"></param>
    /// <returns></returns>
    public static LiteRepository LiteRepositoryFactory(string walletName, SecureString passphrase)
    {
        try
        {
            var connectionString = new ConnectionString
            {
                Filename = WalletPath(walletName),
                Password = passphrase.FromSecureString(),
                Connection = ConnectionType.Shared
            };
            var x = new LiteRepository(connectionString);
            return x;
        }
        catch (Exception)
        {
            // Ignore
        }

        return null;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string WalletPath(string name)
    {
        var wallets = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "wallets");
        var wallet = Path.Combine(wallets, $"{name}.db");
        if (Directory.Exists(wallets)) return wallet;
        try
        {
            Directory.CreateDirectory(wallets);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return wallet;
    }
}