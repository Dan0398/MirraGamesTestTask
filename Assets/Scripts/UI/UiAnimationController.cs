using Dan398.Time;
using DG.Tweening;
using UnityEngine;
using Zenject;
using System;

namespace Dan398.UI
{
    public sealed class UiAnimationController : MonoBehaviour
    {
        [SerializeField] private AnimatedElement[] elements;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Color evenColor = Color.white;
        [SerializeField] private Color oddColor = Color.green;

        [Inject] private AppClock appClock;

        private Tween colorTween;
        private bool colorLocked;

        private void Start()
        {
            appClock.MinuteChanged += OnMinuteChanged;
            foreach (AnimatedElement element in elements)
            {
                element.mask.SetAlpha(0f);
                element.movement.PlayEntrance(duration, element.startDelay);
                Fade(element.mask, 1f, duration).SetDelay(element.startDelay);
            }

            ApplyColor(ParityColor(appClock.Now));
        }

        private void OnDestroy()
        {
            appClock.MinuteChanged -= OnMinuteChanged;
            colorTween?.Kill();
        }

        public void EnterEditMode()
        {
            colorLocked = true;
            foreach (AnimatedElement element in elements)
            {
                element.movement.MoveToEdit(duration);
                Fade(element.mask, element.hideOnEdit ? 0f : 1f, duration);
            }

            ApplyColor(evenColor);
        }

        public void ExitEditMode()
        {
            colorLocked = false;
            foreach (AnimatedElement element in elements)
            {
                element.movement.MoveBack(duration);
                Fade(element.mask, 1f, duration);
            }

            ApplyColor(ParityColor(appClock.Now));
        }

        private void OnMinuteChanged(DateTime time)
        {
            ApplyColor(colorLocked ? evenColor : ParityColor(time));
        }

        private Color ParityColor(DateTime time)
        {
            return time.ToLocalTime().Minute % 2 == 0 ? evenColor : oddColor;
        }

        private void ApplyColor(Color target)
        {
            colorTween?.Kill();
            if (elements.Length == 0)
            {
                Debug.LogError($"[UiAnimationController] elements are not assigned on '{name}'", this);
                return;
            }

            MaskedImageRecolorer mask = elements[0].mask;
            colorTween = DOTween.To(mask.GetColor, SetColorToAll, target, 1f);
        }

        private void SetColorToAll(Color color)
        {
            foreach (AnimatedElement element in elements)
            {
                element.mask.SetColor(color);
            }
        }

        private static Tween Fade(MaskedImageRecolorer mask, float alpha, float fadeDuration)
        {
            DOTween.Kill(mask);
            return DOTween.To(mask.GetAlpha, mask.SetAlpha, alpha, fadeDuration)
                .SetTarget(mask);
        }
    }
}