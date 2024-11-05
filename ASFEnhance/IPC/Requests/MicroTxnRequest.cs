using SteamKit2;
using System.ComponentModel.DataAnnotations;

namespace ASFEnhance.IPC.Requests;

/// <summary>
/// 游戏内购
/// </summary>
public sealed record MicroTxnRequest
{
    /// <summary>
    /// AppId
    /// </summary>
    [Required]
    public uint AppId { get; set; }
    
    [Required]
    public ECurrencyCode Currency  { get; set; }
    
    /// <summary>
    /// 内购物品，每次最多100个
    /// </summary>
    [Required]
    public List<MicroTxnLineItem> LineItems = new();
}

/// <summary>
/// 游戏内购物品
/// </summary>
public sealed record MicroTxnLineItem
{
    [Required]
    public uint DefineID { get; set; }

    [Required]
    public uint Amount { get; set; }
    
    [Required]
    public uint Price { get; set; }
    
    public string? Name { get; set; }
}