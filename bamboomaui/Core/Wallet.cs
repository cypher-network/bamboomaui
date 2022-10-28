using System.Security;
using Bamboomaui.Extensions;
using Bamboomaui.Helpers;
using Bamboomaui.Models;
using Dawn;
using NBitcoin;

namespace Bamboomaui.Core;

public interface IWallet
{
    Task<bool> CreateWallet(in SecureString passphrase, in string walletName);
    Task<TaskResult<string>> CreateWalletSeed(in SecureString seed, in SecureString passphrase, in string walletName);
    KeySet CreateKeySet(KeyPath keyPath, byte[] secretKey, byte[] chainCode);
    string[] CreateSeed(in WordCount wordCount, in Language language);
    ISession Session { get; set; }
}

/// <summary>
/// 
/// </summary>
public class Wallet : IWallet
{
    private NBitcoin.Network _network;
    private ISession _session;
    
    private static int _commandExecutionCounter;

    private static void IncrementCommandExecutionCount()
    {
        ++_commandExecutionCounter;
    }

    /// <summary>
    /// 
    /// </summary>
    private static void DecrementCommandExecutionCount()
    {
        --_commandExecutionCounter;
    }

    /// <summary>
    /// 
    /// </summary>
    public ISession Session
    {
        get => _session;
        set => _session = value;
    }

    public Wallet()
    {
        _network = NBitcoin.Network.TestNet;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="passphrase"></param>
    /// <param name="walletName"></param>
    /// <returns></returns>
    public Task<bool> CreateWallet(in SecureString passphrase, in string walletName)
    {
        var db = Helpers.Util.LiteRepositoryFactory(walletName, passphrase);
        if (db is null) return Task.FromResult(false);
        var keySet = new KeySet();
        db.Insert(keySet);
        _session = new Session(walletName.ToSecureString(), passphrase);
        return Task.FromResult(true);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="passphrase"></param>
    /// <param name="walletName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Task<TaskResult<string>> CreateWalletSeed(in SecureString seed, in SecureString passphrase, in string walletName)
    {
        using var commandExecutionGuard =
            new RAIIGuard(IncrementCommandExecutionCount, DecrementCommandExecutionCount);
        Guard.Argument(seed, nameof(seed)).NotNull();
        Guard.Argument(passphrase, nameof(passphrase)).NotNull();
        seed.MakeReadOnly();
        passphrase.MakeReadOnly();
        try
        {
            CreateRootHdKey(seed, out var hdRoot);
            var keySet = CreateKeySet(new KeyPath($"{Constant.HDPath}0"), hdRoot.PrivateKey.ToHex().HexToByte(),
                hdRoot.ChainCode);
            var db = Helpers.Util.LiteRepositoryFactory(walletName, passphrase);
            db.Insert(keySet);
            keySet.ChainCode.ZeroString();
            keySet.RootKey.ZeroString();
            return Task.FromResult(TaskResult<string>.CreateSuccess(walletName));
        }
        catch (Exception)
        {
            return Task.FromResult(TaskResult<string>.CreateFailure(new Exception("Unable to create wallet.")));
        }
        finally
        {
            seed.Dispose();
            passphrase.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="hdRoot"></param>
    private static void CreateRootHdKey(SecureString seed, out ExtKey hdRoot)
    {
        Guard.Argument(seed, nameof(seed)).NotNull();
        var concatenateMnemonic = string.Join(" ", seed.FromSecureString());
        hdRoot = new Mnemonic(concatenateMnemonic).DeriveExtKey();
        concatenateMnemonic.ZeroString();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyPath"></param>
    /// <param name="secretKey"></param>
    /// <param name="chainCode"></param>
    /// <returns></returns>
    public KeySet CreateKeySet(KeyPath keyPath, byte[] secretKey, byte[] chainCode)
    {
        Guard.Argument(keyPath, nameof(keyPath)).NotNull();
        Guard.Argument(secretKey, nameof(secretKey)).NotNull().MaxCount(32);
        Guard.Argument(chainCode, nameof(chainCode)).NotNull().MaxCount(32);
        try
        {
            var masterKey = new ExtKey(new Key(secretKey), chainCode);
            var spend = masterKey.Derive(keyPath).PrivateKey;
            var scan = masterKey.Derive(keyPath = keyPath.Increment()).PrivateKey;
            return new KeySet
            {
                ChainCode = masterKey.ChainCode.ByteToHex(),
                KeyPath = keyPath.ToString(),
                RootKey = masterKey.PrivateKey.ToHex(),
                StealthAddress = spend.PubKey.CreateStealthAddress(scan.PubKey, _network).ToString(),
            };
        }
        catch (Exception ex)
        {
            var ee = ex;
        }

        return null;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="wordCount"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public string[] CreateSeed(in WordCount wordCount, in Language language)
    {
        var lang = language;
        var task = Task.Run(async () => await Wordlist.LoadWordList(lang));
        task.Wait();
        var mnemonic = new Mnemonic(task.Result, wordCount);
        return mnemonic.Words;
    }
}