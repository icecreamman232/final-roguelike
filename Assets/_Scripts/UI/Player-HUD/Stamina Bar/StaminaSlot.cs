using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class StaminaSlot : MonoBehaviour
    {
        [SerializeField] private Image m_icon;
        [SerializeField] private Sprite m_fullSprite;
        [SerializeField] private Sprite m_emptySprite;

        public void SetFull()
        {
            m_icon.sprite = m_fullSprite;
        }

        public void SetEmpty()
        {
            m_icon.sprite = m_emptySprite;
        }
    }
}

