using DG.Tweening;
using UnityEngine;

namespace Dan398.UI
{
    public sealed class UiElementAnimator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Trs from = new Trs(Vector3.zero, Vector3.zero, Vector3.one);
        [SerializeField] private Trs to = new Trs(Vector3.zero, Vector3.zero, Vector3.one);
        [SerializeField] private Trs onEdit = new Trs(Vector3.zero, Vector3.zero, Vector3.one);
        [SerializeField] private Ease ease = Ease.OutSine;

        private void OnDestroy()
        {
            target.DOKill();
        }

        public Tween PlayEntrance(float duration, float delay)
        {
            Snap(from);
            return Move(to, delay, duration);
        }

        public Tween MoveToEdit(float duration)
        {
            return Move(onEdit, 0f, duration);
        }

        public Tween MoveBack(float duration)
        {
            return Move(to, 0f, duration);
        }

        public void Snap(Trs trs)
        {
            target.DOKill();
            target.localPosition = trs.position;
            target.localEulerAngles = trs.rotation;
            target.localScale = trs.scale;
        }

        private Tween Move(Trs destination, float delay, float duration)
        {
            target.DOKill();
            Sequence sequence = DOTween.Sequence().SetTarget(target).SetDelay(delay);
            sequence.Append(target.DOLocalMove(destination.position, duration).SetEase(ease));
            sequence.Join(target.DOLocalRotate(destination.rotation, duration).SetEase(ease));
            sequence.Join(target.DOScale(destination.scale, duration).SetEase(ease));
            return sequence;
        }
    }
}