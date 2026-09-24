using System;

namespace Dan398.UI
{
    [Serializable]
    public sealed class AnimatedElement
    {
        public MaskedImageRecolorer mask;
        public UiElementAnimator movement;
        public float startDelay;
        public bool hideOnEdit;
    }
}