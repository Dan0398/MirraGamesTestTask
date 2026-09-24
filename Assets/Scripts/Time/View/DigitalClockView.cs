using UnityEngine;
using Zenject;
using TMPro;
using System;

namespace Dan398.Time.View
{
    public sealed class DigitalClockView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private string format = "HH:mm:ss";

        [Inject] private AppClock appClock;

        private void Start()
        {
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