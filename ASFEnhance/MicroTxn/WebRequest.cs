using ArchiSteamFarm.IPC.Responses;
using ArchiSteamFarm.Steam;
using ASFEnhance.IPC.Requests;
using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.CSGO.Internal;
using SteamKit2.Internal;

namespace ASFEnhance.MicroTxn;

internal static class WebRequest
{
    /// <summary>
    /// 游戏内购,
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="steamId"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    internal static async Task<GenericResponse<int?>> Purchase(this Bot bot, MicroTxnRequest request)
    {
        var lineItems = request.LineItems;
        foreach (var lineItem in lineItems)
        {
            if (lineItem.Amount <= 0) return new GenericResponse<int?>(false, "数量不正确", null);
            if (lineItem.Price <= 0) return new GenericResponse<int?>(false, "价格不正确", null);
        }

        return new GenericResponse<int?>(false, "游戏内购", null);
    }


}