using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class HealthBarUIController : MonoBehaviour
    {
        [SerializeField] private Image m_healthBar;
        [SerializeField] private Image m_border;
        [SerializeField] private TextMeshProUGUI m_healthText;
        [SerializeField] private PlayerHealthUpdateEvent m_playerHealthUpdateEvent;
        [SerializeField] private BoolEvent m_playerGainImmortalEvent;
        
        private void Awake()
        {
            m_playerHealthUpdateEvent.AddListener(OnPlayerHealthUpdate);
            m_playerGainImmortalEvent.AddListener(OnPlayerGainImmortal);
            
        }
        
        private void OnDestroy()
        {
            m_playerHealthUpdateEvent.RemoveListener(OnPlayerHealthUpdate);
            m_playerGainImmortalEvent.RemoveListener(OnPlayerGainImmortal);
        }
        
        private void OnPlayerHealthUpdate(float current, float max, GameObject source)
        {
            m_healthBar.fillAmount = MathHelpers.Remap(current, 0f, max, 0f, 1f);
            if (current <= 0)
            {
                current = 0;
            }
            m_healthText.text = $"{current:F0}/{max:F0}";
        }
        
        private void OnPlayerGainImmortal(bool isImmortal)
        {
            m_border.color=  isImmortal ? Color.yellow : Color.white;
        }
    }
}

