using DG.Tweening;
using UnityEngine;
using Zenject;
using System;

namespace Dan398.Time.View
{
    public sealed class AnalogClockView : MonoBehaviour
    {
        [SerializeField] private Transform hourArrow;
        [SerializeField] private Transform minuteArrow;
        [SerializeField] private Transform secondArrow;
        [SerializeField] private bool smoothSecond;
        [SerializeField] private float secondStepDuration = 0.25f;

        [Inject] private AppClock appClock;

        private float secondArrowAngle;
        private bool drafting;
        private DateTime draftTime;

        private void Start()
        {
            secondArrowAngle = -(float)(appClock.Now.ToLocalTime().TimeOfDay.TotalSeconds % 60) * 6f;
            secondArrow.localEulerAngles = new Vector3(0f, 0f, secondArrowAngle);
            if (!smoothSecond)
            {
                appClock.SecondChanged += OnSecondChanged;
            }
        }

        private void Update()
        {
            DateTime time = drafting ? draftTime : appClock.Now.ToLocalTime();
            TimeSpan timeOfDay = time.TimeOfDay;
            hourArrow.localEulerAngles = new Vector3(0f, 0f, -(float)(timeOfDay.TotalHours % 12) * 30f);
            minuteArrow.localEulerAngles = new Vector3(0f, 0f, -(float)(timeOfDay.TotalMinutes % 60) * 6f);
            if (smoothSecond || drafting)
            {
                secondArrow.localEulerAngles = new Vector3(0f, 0f, -(float)(timeOfDay.TotalSeconds % 60) * 6f);
            }
        }

        private void OnDestroy()
        {
            secondArrow.DOKill();
            if (!smoothSecond)
            {
                appClock.SecondChanged -= OnSecondChanged;
            }
        }

        public void ShowDraft(DateTime time)
        {
            drafting = true;
            draftTime = time;
            secondArrow.DOKill();
        }

        public void ShowLive()
        {
            drafting = false;
            secondArrowAngle = -(float)(appClock.Now.ToLocalTime().TimeOfDay.TotalSeconds % 60) * 6f - 360f;
        }

        private void OnSecondChanged(DateTime time)
        {
            if (drafting) return;
            DateTime localTime = time.ToLocalTime();
            float targetAngle = -(float)(localTime.TimeOfDay.TotalSeconds % 60) * 6f;
            while (targetAngle > secondArrowAngle)
            {
                targetAngle -= 360f;
            }

            secondArrow.DOKill();
            DOTween.To(() => secondArrowAngle, ApplySecondArrow, targetAngle, secondStepDuration)
                .SetEase(Ease.OutBack)
                .SetTarget(secondArrow);
        }

        private void ApplySecondArrow(float angle)
        {
            secondArrowAngle = angle;
            secondArrow.localEulerAngles = new Vector3(0f, 0f, angle);
        }
    }
}