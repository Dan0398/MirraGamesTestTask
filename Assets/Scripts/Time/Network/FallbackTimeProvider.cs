using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

namespace Dan398.Time.Network
{
    public sealed class FallbackTimeProvider : IServerTimeProvider
    {
        private readonly IReadOnlyList<IServerTimeProvider> providers;

        public FallbackTimeProvider(IReadOnlyList<IServerTimeProvider> providers)
        {
            this.providers = providers;
        }

        public string SourceName => "fallback";

        public async Task<DateTime> GetServerTimeAsync()
        {
            foreach (IServerTimeProvider provider in providers)
            {
                try
                {
                    DateTime serverTime = await provider.GetServerTimeAsync();
                    Debug.Log($"[Time] source '{provider.SourceName}' synced: {serverTime:yyyy-MM-dd HH:mm:ss.fff} UTC");
                    return serverTime;
                }
                catch (Exception exception)
                {
                    Debug.LogWarning($"[Time] source '{provider.SourceName}' failed: {exception.Message}");
                }
            }

            throw new InvalidOperationException("All time sources failed");
        }
    }
}