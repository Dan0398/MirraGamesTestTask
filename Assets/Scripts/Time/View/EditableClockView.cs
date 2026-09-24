using Dan398.Time.Controller;
using UnityEngine;
using System;

namespace Dan398.Time.View
{
    public sealed class EditableClockView : MonoBehaviour
    {
        [SerializeField] private ClockEditController controller;
        [SerializeField] private AnalogClockView clockView;
        [SerializeField] private ArrowDragInput hourArrow;
        [SerializeField] private ArrowDragInput minuteArrow;

        private float hourDegrees;
        private float minuteDegrees;

        private void Start()
        {
            controller.DraftChanged += OnDraftChanged;
            hourArrow.Dragged += OnHourDragged;
            minuteArrow.Dragged += OnMinuteDragged;
        }

        private void OnDestroy()
        {
            controller.DraftChanged -= OnDraftChanged;
            hourArrow.Dragged -= OnHourDragged;
            minuteArrow.Dragged -= OnMinuteDragged;
        }

        public void SetEditing(bool editing)
        {
            hourArrow.enabled = editing;
            minuteArrow.enabled = editing;
            hourDegrees = 0f;
            minuteDegrees = 0f;
            if (!editing)
            {
                clockView.ShowLive();
            }
        }

        private void OnDraftChanged(DateTime draft)
        {
            clockView.ShowDraft(draft);
        }

        private void OnHourDragged(float deltaDegrees)
        {
            hourDegrees += deltaDegrees;
            int hours = (int)(hourDegrees / 30f);
            if (hours == 0)
            {
                return;
            }

            hourDegrees -= hours * 30f;
            controller.AddDraftHours(hours);
        }

        private void OnMinuteDragged(float deltaDegrees)
        {
            minuteDegrees += deltaDegrees;
            int minutes = (int)(minuteDegrees / 6f);
            if (minutes == 0)
            {
                return;
            }

            minuteDegrees -= minutes * 6f;
            controller.AddDraftMinutes(minutes);
        }
    }
}