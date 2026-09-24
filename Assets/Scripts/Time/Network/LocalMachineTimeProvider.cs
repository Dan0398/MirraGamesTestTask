using System.Threading.Tasks;
using System;

namespace Dan398.Time.Network
{
    public sealed class LocalMachineTimeProvider : IServerTimeProvider
    {
        public string SourceName => "local";

        public Task<DateTime> GetServerTimeAsync()
        {
            return Task.FromResult(DateTime.UtcNow);
        }
    }
}