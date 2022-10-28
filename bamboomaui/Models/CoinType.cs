namespace Bamboomaui.Models;

/// <summary>
/// 
/// </summary>
public enum CoinType : sbyte
{
    Empty = 0,
    Coin = 1,
    Coinbase = 2,
    Coinstake = 3,
    Fee = 4,
    Genesis = 5,
    Payment = 6,
    Change = 7,
    Timebase = 8
}