using Zenject;
using System;

namespace Dan398.Time
{
    public sealed class AppClock : ITickable
    {
        private DateTime baseTime = DateTime.UtcNow;
        private double baseRealtime = UnityEngine.Time.realtimeSinceStartupAsDouble;
        private long lastSecond;

        public event Action<DateTime> SecondChanged;

        public event Action<DateTime> MinuteChanged;

        public bool IsManuallySet { get; private set; }

        public DateTime Now => baseTime.AddSeconds(UnityEngine.Time.realtimeSinceStartupAsDouble - baseRealtime);

        public void Sync(DateTime sourceTime)
        {
            SetBase(sourceTime);
            IsManuallySet = false;
        }

        public void SetManual(DateTime manualTime)
        {
            SetBase(manualTime);
            IsManuallySet = true;
        }

        void ITickable.Tick()
        {
            DateTime currentTime = Now;
            long second = currentTime.Ticks / TimeSpan.TicksPerSecond;
            if (second == lastSecond)
            {
                return;
            }

            lastSecond = second;

            SecondChanged?.Invoke(currentTime);
            if (currentTime.Second == 0)
            {
                MinuteChanged?.Invoke(currentTime);
            }
        }

        private void SetBase(DateTime time)
        {
            baseTime = time;
            baseRealtime = UnityEngine.Time.realtimeSinceStartupAsDouble;
        }
    }
}