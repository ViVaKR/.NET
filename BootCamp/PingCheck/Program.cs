using System.Net.NetworkInformation;

bool isConnected = IsInternetConnected("8.8.8.8").Result;

var message = isConnected ? "Connected" : "Disconnected";
Console.WriteLine($"Internet is {message}");

/// <summary>
/// 인터넷 연결여부 확인
/// </summary>
/// <param name="url">IP Address or DNS Domain</param>
/// <returns></returns>
static Task<bool> IsInternetConnected(string url)
{
    return Task.Run(() =>
    {
        try
        {
            Ping myPing = new();
            byte[] buffer = new byte[32];
            const int timeout = 1000;
            PingOptions pingOptions = new()
            {
                DontFragment = true,
                Ttl = 64
            };
            PingReply reply = myPing.Send(url, timeout, buffer, pingOptions);
            return reply?.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    });
}
