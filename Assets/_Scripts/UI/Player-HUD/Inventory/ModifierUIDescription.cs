using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class ModifierUIDescription : MonoBehaviour
    {
        [SerializeField ]private TextMeshProUGUI m_description;
        private readonly int C_CONTENT_ROW_HEIGHT = 50;
        
        public void FillDescription(string description)
        {
            m_description.text = description;
            m_description.ForceMeshUpdate(true);
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
