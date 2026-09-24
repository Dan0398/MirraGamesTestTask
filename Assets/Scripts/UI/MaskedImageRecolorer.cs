using UnityEngine.UI;
using UnityEngine;
using System;

namespace Dan398.UI
{
    public sealed class MaskedImageRecolorer : MonoBehaviour
    {
        private MaskableGraphic[] graphics;
        private Color[] initialColors;

        private void Awake()
        {
            graphics = GetComponentsInChildren<MaskableGraphic>(true);
            initialColors = new Color[graphics.Length];
            for (int i = 0; i < graphics.Length; i++)
            {
                initialColors[i] = graphics[i].color;
            }
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                Color current = graphics[i].color;
                graphics[i].color = new Color(color.r, color.g, color.b, current.a);
            }
        }

        public void RestoreColors()
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].color = initialColors[i];
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

        public float GetAlpha()
        {
            return graphics[0].color.a;
        }

        public Color GetColor()
        {
            return graphics[0].color;
        }
    }
}