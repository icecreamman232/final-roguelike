using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class ModifierUIDescription : MonoBehaviour
    {
        private TextMeshProUGUI m_description;
        private readonly int C_CONTENT_ROW_HEIGHT = 50;

        private void Start()
        {
            m_description = GetComponent<TextMeshProUGUI>();
        }

        public void FillDescription(string description)
        {
            m_description.text = description;
            ResizeHeight();
        }

        private void ResizeHeight()
        {
            var currentSize = ((RectTransform)transform).sizeDelta;
            currentSize.y = GetContentHeight();
            ((RectTransform)transform).sizeDelta = currentSize;
        }

        private float GetContentHeight()
        {
            return m_description.textInfo.lineCount * C_CONTENT_ROW_HEIGHT;
        }
    }
}
