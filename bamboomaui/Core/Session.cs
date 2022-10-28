using System.Security;
using Bamboomaui.Extensions;
using Bamboomaui.Helpers;
using Bamboomaui.Models;
using LiteDB;

namespace Bamboomaui.Core;

/// <summary>
/// 
/// </summary>
public interface ISession
{
    SecureString Name { get; }
    SecureString Passphrase { get; }
    SecureString Seed { get; }
    Guid SessionId { get; set; }
    CoinType CoinType { get; set; }
    LiteRepository Database { get; }
    KeySet KeySet { get; }
    bool IsValid { get; }
}

/// <summary>
/// 
/// </summary>
public class Session : ISession
{
    public SecureString Name { get; }
    public SecureString Passphrase { get; }
    public SecureString Seed { get; }
    public Guid SessionId { get; set; }
    public CoinType CoinType { get; set; }
    public LiteRepository Database { get; set; }

    public KeySet KeySet => Database.Query<KeySet>().First();
    public bool IsValid => IsIdentifierValid(Name);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="passphrase"></param>
    /// <exception cref="FileLoadException"></exception>
    public Session(SecureString name, SecureString passphrase)
    {
        Name = name;
        Passphrase = passphrase;
        SessionId = Guid.NewGuid();
        CreateDb(name, passphrase);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="passphrase"></param>
    /// <param name="seed"></param>
    /// <exception cref="FileLoadException"></exception>
    public Session(SecureString name, SecureString passphrase, SecureString seed)
    {
        Name = name;
        Passphrase = passphrase;
        Seed = seed;
        SessionId = Guid.NewGuid();
        CreateDb(name, passphrase);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="passphrase"></param>
    /// <returns></returns>
    public static bool AreCredentialsValid(SecureString identifier, SecureString passphrase)
    {
        return IsIdentifierValid(identifier) && IsPassPhraseValid(identifier, passphrase);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    private static bool IsPassPhraseValid(SecureString id, SecureString pass)
    {
        var connectionString = new ConnectionString
        {
            Filename = Util.WalletPath(id.FromSecureString()),
            Password = pass.FromSecureString(),
            Connection = ConnectionType.Shared
        };
        using var db = new LiteDatabase(connectionString);
        var collection = db.GetCollection<KeySet>();
        try
        {
            if (collection.Count() == 1)
            {
                return true;
            }
        }
        catch (LiteException)
        {
            return false;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    private static bool IsIdentifierValid(SecureString identifier)
    {
        return File.Exists(Util.WalletPath(identifier.FromSecureString()));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="passphrase"></param>
    /// <exception cref="FileLoadException"></exception>
    private void CreateDb(SecureString name, SecureString passphrase)
    {
        if (!IsValid)
        {
            throw new FileLoadException($"Wallet: {name.FromSecureString()} not found!");
        }

        Database = Util.LiteRepositoryFactory(name.FromSecureString(), passphrase);
    }
}