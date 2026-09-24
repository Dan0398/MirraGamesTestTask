using UnityEngine.UI;
using UnityEngine;

namespace Dan398.UI
{
    public sealed class RecolorProxy : MonoBehaviour
    {
        [SerializeField] private MaskableGraphic[] graphics;
        [SerializeField] private Color hoverColor = Color.yellow;

        private Color[] initialColors;
        private Color groupColor;
        private bool hovered;

        private void Awake()
        {
            initialColors = new Color[graphics.Length];
            for (int i = 0; i < graphics.Length; i++)
            {
                initialColors[i] = graphics[i].color;
            }

            groupColor = graphics[0].color;
        }

        public bool Owns(MaskableGraphic graphic)
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                if (graphics[i] == graphic)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetColor(Color color)
        {
            groupColor = color;
            if (!hovered)
            {
                ApplyColor(color);
            }
        }

        public void SetAlpha(float alpha)
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                Color current = graphics[i].color;
                graphics[i].color = new Color(current.r, current.g, current.b, alpha);
            }
        }

        public void RestoreColors()
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].color = initialColors[i];
            }
        }

        public void HoverEntered()
        {
            hovered = true;
            ApplyColor(hoverColor);
        }

        public void HoverExited()
        {
            hovered = false;
            ApplyColor(groupColor);
        }

        private void ApplyColor(Color color)
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                Color current = graphics[i].color;
                graphics[i].color = new Color(color.r, color.g, color.b, current.a);
            }
        }
    }
}