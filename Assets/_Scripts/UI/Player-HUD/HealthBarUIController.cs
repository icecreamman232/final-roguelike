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
        [SerializeField] private TextMeshProUGUI m_healthText;
        [SerializeField] private PlayerHealthUpdateEvent m_playerHealthUpdateEvent;
        
        
        private void Awake()
        {
            m_playerHealthUpdateEvent.AddListener(OnPlayerHealthUpdate);
            
        }

        private void OnDestroy()
        {
            m_playerHealthUpdateEvent.RemoveListener(OnPlayerHealthUpdate);
        }
        
        private void OnPlayerHealthUpdate(float current, float max, GameObject source)
        {
            m_healthBar.fillAmount = MathHelpers.Remap(current, 0f, max, 0f, 1f);
            m_healthText.text = $"{current:F0}/{max}";
        }
    }
}

