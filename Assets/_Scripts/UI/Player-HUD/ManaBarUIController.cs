using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class ManaBarUIController : MonoBehaviour
    {
        [SerializeField] private Image m_manaBar;
        [SerializeField] private Image m_border;
        [SerializeField] private TextMeshProUGUI m_manaText;
        [SerializeField] private PlayerManaUpdateEvent m_playerManaUpdateEvent;
        private void Awake()
        {
            m_playerManaUpdateEvent.AddListener(OnPlayerManaUpdate);
        }
        
        private void OnDestroy()
        {
            m_playerManaUpdateEvent.RemoveListener(OnPlayerManaUpdate);
        }
        
        private void OnPlayerManaUpdate(float current, float max)
        {
            m_manaBar.fillAmount = MathHelpers.Remap(current, 0f, max, 0f, 1f);
            if (current <= 0)
            {
                current = 0;
            }
            m_manaText.text = $"{current:F0}/{max:F0}";
        }
    }
}