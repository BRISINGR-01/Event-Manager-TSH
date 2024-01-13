using System.Collections.Concurrent;

namespace Logic.Utilities
{
    public class IpHandler
    {
        private readonly ConcurrentQueue<string> bannedAdderesses = new();
        private readonly ConcurrentDictionary<string, IpData> ips = new();
        public bool IsBanned(string ip)
        {
            return bannedAdderesses.Contains(ip);
        }

        public bool CheckIp(string ip)
        {
            if (!ips.TryGetValue(ip, out IpData? ipData))
            {
                ips.TryAdd(ip, new IpData(() => ips.TryRemove(ip, out _)));
                return true;
            }

            if (ipData.Count < 20)
            {
                ipData.Update();
                return true;
            }

            bannedAdderesses.Enqueue(ip);
            new Thread(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(20));
                bannedAdderesses.TryDequeue(out string? _);
                ipData.Remove();
            }).Start();

            return false;
        }
    }
}
