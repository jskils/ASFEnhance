namespace ASFEnhance.Data.Common;

/// <summary>
/// 
/// </summary>
public static class RequestHeader
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly Dictionary<string, string> UserAgentHeader = new(5)
    {
        { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36" },
        { "Sec-Ch-Ua", "\"Not.A/Brand\";v=\"8\", \"Chromium\";v=\"114\", \"Google Chrome\";v=\"114\"" },
        { "Sec-Ch-Ua-Mobile", "?0" },
        { "Sec-Ch-Ua-Platform", "\"Windows\"" },
        { "Accept-Language", "zh-CN,zh;q=0.9" }
    };
}