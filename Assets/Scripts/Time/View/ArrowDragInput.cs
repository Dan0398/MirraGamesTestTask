using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using System;

namespace Dan398.Time.View
{
    public sealed class ArrowDragInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform dial;
        [SerializeField] private float deadZone = 30f;
        [SerializeField] private UnityEvent hoverEntered;
        [SerializeField] private UnityEvent hoverExited;

        public event Action<float> Dragged;

        private bool dragging;
        private float previousAngle;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!TryGetAngle(eventData, out float angle))
            {
                return;
            }

            dragging = true;
            previousAngle = angle;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!dragging || !TryGetAngle(eventData, out float angle))
            {
                return;
            }

            float delta = Mathf.DeltaAngle(previousAngle, angle);
            previousAngle = angle;
            if (Mathf.Abs(delta) > 0.01f)
            {
                Dragged?.Invoke(delta);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hoverEntered?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hoverExited?.Invoke();
        }

        private bool TryGetAngle(PointerEventData eventData, out float angle)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(dial, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                angle = 0f;
                return false;
            }

            Vector2 delta = localPoint - dial.rect.center;
            if (delta.sqrMagnitude < deadZone * deadZone)
            {
                angle = 0f;
                return false;
            }

            angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg;
            return true;
        }
    }
}