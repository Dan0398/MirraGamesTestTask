using Dan398.Time.Controller;
using UnityEngine;
using System;
using TMPro;

namespace Dan398.Time.View
{
    public sealed class EditPanelView : MonoBehaviour
    {
        [SerializeField] private ClockEditController controller;
        [SerializeField] private GameObject idleGroup;
        [SerializeField] private GameObject editGroup;
        [SerializeField] private TMP_InputField yearField;
        [SerializeField] private TMP_InputField monthField;
        [SerializeField] private TMP_InputField dayField;
        [SerializeField] private TMP_InputField hourField;
        [SerializeField] private TMP_InputField minuteField;
        [SerializeField] private TMP_InputField secondField;

        private DateTime lastDraft;

        private void Start()
        {
            controller.DraftChanged += OnDraftChanged;
        }

        private void OnDestroy()
        {
            controller.DraftChanged -= OnDraftChanged;
        }

        public void SetEditing(bool editing)
        {
            idleGroup.SetActive(!editing);
            editGroup.SetActive(editing);
        }

        public void Button_EditClicked()
        {
            controller.EnterEdit();
        }

        public void Button_ApplyClicked()
        {
            controller.Apply();
        }

        public void Button_CancelClicked()
        {
            controller.Cancel();
        }

        public void Button_ResetClicked()
        {
            controller.Resync();
        }

        public void InputField_AnyFieldEdited()
        {
            if (TryReadFields(out DateTime value))
            {
                controller.SetDraft(value);
                return;
            }

            OnDraftChanged(lastDraft);
        }

        public void InputField_DayValueChanged()
        {
            if (!int.TryParse(dayField.text, out int day))
            {
                return;
            }

            int maxDay = DateTime.DaysInMonth(lastDraft.Year, lastDraft.Month);
            if (day > maxDay)
            {
                dayField.SetTextWithoutNotify(maxDay.ToString());
            }
        }

        private void OnDraftChanged(DateTime draft)
        {
            lastDraft = draft;
            yearField.SetTextWithoutNotify(draft.Year.ToString());
            monthField.SetTextWithoutNotify(draft.Month.ToString("00"));
            dayField.SetTextWithoutNotify(draft.Day.ToString("00"));
            hourField.SetTextWithoutNotify(draft.Hour.ToString("00"));
            minuteField.SetTextWithoutNotify(draft.Minute.ToString("00"));
            secondField.SetTextWithoutNotify(draft.Second.ToString("00"));
        }

        private bool TryReadFields(out DateTime value)
        {
            if (!int.TryParse(yearField.text, out int year) || !int.TryParse(monthField.text, out int month)
                || !int.TryParse(dayField.text, out int day) || !int.TryParse(hourField.text, out int hour)
                || !int.TryParse(minuteField.text, out int minute) || !int.TryParse(secondField.text, out int second))
            {
                value = default;
                return false;
            }

            year = Mathf.Clamp(year, 1, 9999);
            month = Mathf.Clamp(month, 1, 12);
            day = Mathf.Clamp(day, 1, DateTime.DaysInMonth(year, month));
            hour = Mathf.Clamp(hour, 0, 23);
            minute = Mathf.Clamp(minute, 0, 59);
            second = Mathf.Clamp(second, 0, 59);
            value = new DateTime(year, month, day, hour, minute, second);
            return true;
        }
    }
}