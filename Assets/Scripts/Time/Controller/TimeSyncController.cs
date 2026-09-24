using System.Threading.Tasks;
using Dan398.Time.Network;
using UnityEngine;
using Zenject;
using System;

namespace Dan398.Time.Controller
{
    public sealed class TimeSyncController : IInitializable
    {
        private readonly IServerTimeProvider timeProvider;
        private readonly AppClock appClock;

        public TimeSyncController(IServerTimeProvider timeProvider, AppClock appClock)
        {
            this.timeProvider = timeProvider;
            this.appClock = appClock;
        }

        void IInitializable.Initialize()
        {
            _ = SyncWithServerAsync();
        }

        public async Task SyncWithServerAsync()
        {
            try
            {
                appClock.Sync(await timeProvider.GetServerTimeAsync());
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Time] sync failed: {exception.Message}");
            }
        }
    }
}