using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class InventoryCursor : MonoBehaviour
    {
        [SerializeField] private Image m_icon;
        [SerializeField] private bool m_hasIcon;
        [SerializeField] private Canvas m_canvas;
        
        public void SetIcon(Sprite icon)
        {
            m_icon.sprite = icon;
            m_icon.color = icon != null ? Color.white : Color.clear;
            m_hasIcon = icon != null;
        }

        private void Update()
        {
            if (!m_hasIcon) return;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvas.transform as RectTransform, Input.mousePosition, m_canvas.worldCamera, out var pos);
            transform.position = m_canvas.transform.TransformPoint(pos);
        }
    }
}

