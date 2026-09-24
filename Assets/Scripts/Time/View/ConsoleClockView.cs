using UnityEngine;
using Zenject;
using System;

namespace Dan398.Time.View
{
    public sealed class ConsoleClockView : IInitializable, IDisposable
    {
        private readonly AppClock appClock;

        public ConsoleClockView(AppClock appClock)
        {
            this.appClock = appClock;
        }

        void IInitializable.Initialize()
        {
            appClock.SecondChanged += OnSecondChanged;
        }

        void IDisposable.Dispose()
        {
            appClock.SecondChanged -= OnSecondChanged;
        }

        private void OnSecondChanged(DateTime time)
        {
            Debug.Log($"[Clock] {time.ToLocalTime():yyyy-MM-dd HH:mm:ss}");
        }
    }
}