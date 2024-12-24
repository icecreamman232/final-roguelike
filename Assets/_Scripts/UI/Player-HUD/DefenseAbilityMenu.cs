using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class DefenseAbilityMenu : MonoBehaviour
    {
        [SerializeField] private Image m_disableAbilityIcon;
        [SerializeField] private Image m_enableAbilityIcon;
        [SerializeField] private AbilityCoolDownEvent m_abilityCoolDownEvent;

        private void Start()
        {
            var heroData = LevelManager.Instance.HeroData;
            m_disableAbilityIcon.sprite = heroData.DefenseAbilityDisableSprite;
            m_enableAbilityIcon.sprite = heroData.DefenseAbilityEnableSprite;
            
            m_enableAbilityIcon.fillAmount = 1f;
            m_abilityCoolDownEvent.AddListener(OnUpdateCoolDownUI);
        }

        private void OnDestroy()
        {
            m_abilityCoolDownEvent.RemoveListener(OnUpdateCoolDownUI);
        }

        private void OnUpdateCoolDownUI(float current, float max)
        {
            m_enableAbilityIcon.fillAmount = 1 - MathHelpers.Remap(current,0,max,0,1);
        }
    }
}

