using Dan398.Time.View;
using UnityEngine;
using Dan398.UI;
using Zenject;
using System;

namespace Dan398.Time.Controller
{
    public sealed class ClockEditController : MonoBehaviour
    {
        [SerializeField] private UiAnimationController animationController;
        [SerializeField] private EditableClockView editableClock;
        [SerializeField] private EditPanelView panel;

        [Inject] private AppClock appClock;

        public event Action<DateTime> DraftChanged;

        private DateTime draft;
        private bool editing;

        public void EnterEdit()
        {
            editing = true;
            draft = appClock.Now.ToLocalTime();
            animationController.EnterEditMode();
            panel.SetEditing(true);
            editableClock.SetEditing(true);
            DraftChanged?.Invoke(draft);
        }

        public void Apply()
        {
            if (!editing) return;
            appClock.SetManual(draft.ToUniversalTime());
            Exit();
        }

        public void Cancel()
        {
            if (!editing) return;
            Exit();
        }

        public void AddDraftHours(int delta)
        {
            if (!editing) return;
            draft = draft.AddHours(delta);
            DraftChanged?.Invoke(draft);
        }

        public void AddDraftMinutes(int delta)
        {
            if (!editing) return;
            draft = draft.AddMinutes(delta);
            DraftChanged?.Invoke(draft);
        }

        public void SetDraft(DateTime value)
        {
            if (!editing) return;
            draft = value;
            DraftChanged?.Invoke(draft);
        }

        private void Exit()
        {
            editing = false;
            animationController.ExitEditMode();
            panel.SetEditing(false);
            editableClock.SetEditing(false);
        }
    }
}