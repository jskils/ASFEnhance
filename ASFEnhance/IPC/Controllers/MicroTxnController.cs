using ArchiSteamFarm.Core;
using ArchiSteamFarm.IPC.Responses;
using ArchiSteamFarm.Localization;
using ArchiSteamFarm.Steam;
using ASFEnhance.IPC.Requests;
using ASFEnhance.IPC.Responses;
using Microsoft.AspNetCore.Mvc;
using SteamKit2;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;
using System.Globalization;
using System.Net;

namespace ASFEnhance.IPC.Controllers;

/// <summary>
/// 游戏内购相关接口
/// </summary>
public sealed class MicroTxnController : ASFEController
{
    /// <summary>
    /// 游戏内购
    /// </summary>
    /// <param name="botNames"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost("{botNames:required}")]
    [SwaggerOperation(Summary = "游戏内购", Description = "游戏内购")]
    [ProducesResponseType(typeof(GenericResponse<IReadOnlyDictionary<string, BoolDictResponse>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GenericResponse>> Purchase(string botNames, [FromBody] MicroTxnRequest request)
    {
        if (string.IsNullOrEmpty(botNames))
        {
            throw new ArgumentNullException(nameof(botNames));
        }

        if (request.Currency == ECurrencyCode.Invalid)
        {
            throw new ArgumentNullException(nameof(botNames));
        }

        if (request.LineItems is not { Count: > 0 } || request.LineItems.Count > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(botNames));
        }

        ArgumentNullException.ThrowIfNull(request);

        HashSet<Bot>? bots = Bot.GetBots(botNames);

        if (bots == null || bots.Count == 0)
        {
            return BadRequest(new GenericResponse(false, string.Format(CultureInfo.CurrentCulture, Strings.BotNotFound, botNames)));
        }

        IList<(string BotName, GenericResponse<int?> Data)> results = await Utilities.InParallel(bots.Select(
            async bot =>
            {
                if (!bot.IsConnectedAndLoggedOn) { return (bot.BotName, new GenericResponse<int?>(false, Strings.BotNotConnected)); }

                if (bot.IsAccountLocked) { return (bot.BotName, new GenericResponse<int?>(false, "Account Banned")); }

                var result = await MicroTxn.WebRequest.Purchase(bot, request).ConfigureAwait(false);
                return (bot.BotName, result);
            }
        )).ConfigureAwait(false);

        Dictionary<string, GenericResponse<int?>> response = results.ToDictionary(static x => x.BotName, static x => x.Data);
        return Ok(new GenericResponse<IReadOnlyDictionary<string, GenericResponse<int?>>>(response));
    }
}