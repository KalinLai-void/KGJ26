using KalinKonta;
using UnityEngine;

namespace KalinKonta
{
    public class Highlighter : MonoBehaviour
    {
        public Outline.Mode OutlineMode;
        public Color OutlineColor;
        public float OutlineWidth;

        private Outline[] outlines;

        public void ToggleHighlight(bool isOn)
        {
            outlines = GetComponentsInChildren<Outline>();

            foreach (var outline in outlines)
            {
                Debug.Log(outline);
                outline.enabled = isOn;

                if (isOn)
                {
                    outline.OutlineMode = OutlineMode;
                    outline.OutlineColor = OutlineColor;
                    outline.OutlineWidth = OutlineWidth;
                }
            }
        }
    }
}