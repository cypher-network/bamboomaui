using LiteDB;

namespace Bamboomaui.Models;

/// <summary>
/// 
/// </summary>
public class KeySet
{
    [BsonId] public Guid id { get; set; }
    public string ChainCode { get; set; }
    public string KeyPath { get; set; }
    public string RootKey { get; set; }
    public string StealthAddress { get; set; }
    public string Seed { get; set; }
}