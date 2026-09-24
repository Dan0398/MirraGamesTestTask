using UnityEngine;
using Zenject;
using System;
using TMPro;

namespace Dan398.Time.View
{
    public sealed class DigitalClockView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private string format = "HH:mm:ss";

        [Inject] private AppClock appClock;

        private void Start()
        {
            OnSecondChanged(appClock.Now);
            appClock.SecondChanged += OnSecondChanged;
        }

        private void OnDestroy()
        {
            appClock.SecondChanged -= OnSecondChanged;
        }

        private void OnSecondChanged(DateTime time)
        {
            label.text = time.ToLocalTime().ToString(format);
        }
    }
}