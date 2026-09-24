using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Dan398.UI
{
    public sealed class MaskedImageRecolorer : MonoBehaviour
    {
        private MaskableGraphic[] graphics;
        private RecolorProxy[] proxies;
        private Color[] initialColors;

        private void Awake()
        {
            proxies = GetComponentsInChildren<RecolorProxy>(true);
            List<MaskableGraphic> collected = new List<MaskableGraphic>(GetComponentsInChildren<MaskableGraphic>(true));
            for (int i = collected.Count - 1; i >= 0; i--)
            {
                for (int p = 0; p < proxies.Length; p++)
                {
                    if (proxies[p].Owns(collected[i]))
                    {
                        collected.RemoveAt(i);
                        break;
                    }
                }
            }

            graphics = collected.ToArray();
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

            for (int i = 0; i < proxies.Length; i++)
            {
                proxies[i].SetColor(color);
            }
        }

        public void RestoreColors()
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].color = initialColors[i];
            }

            for (int i = 0; i < proxies.Length; i++)
            {
                proxies[i].RestoreColors();
            }
        }

        public void SetAlpha(float alpha)
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                Color current = graphics[i].color;
                graphics[i].color = new Color(current.r, current.g, current.b, alpha);
            }

            for (int i = 0; i < proxies.Length; i++)
            {
                proxies[i].SetAlpha(alpha);
            }
        }

        public float GetAlpha()
        {
            return graphics.Length > 0 ? graphics[0].color.a : 1f;
        }

        public Color GetColor()
        {
            return graphics.Length > 0 ? graphics[0].color : Color.white;
        }
    }
}