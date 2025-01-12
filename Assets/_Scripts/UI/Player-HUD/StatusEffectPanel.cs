using SGGames.Scripts.Events;
using SGGames.Scripts.StatusEffects;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class StatusEffectPanel : MonoBehaviour
    {
        [SerializeField] private StatusEffectEvent m_StatusEffectEvent;
        [SerializeField] private StatusEffectUINode m_burnNode;
        [SerializeField] private StatusEffectUINode m_poisonNode;
        [SerializeField] private StatusEffectUINode m_bleedNode;

        private void Start()
        {
            m_StatusEffectEvent.AddListener(OnStatusEffectEvent);
            m_burnNode.Hide();
            m_poisonNode.Hide();
            m_bleedNode.Hide();
        }
        
        private void OnDestroy()
        {
            m_StatusEffectEvent.RemoveListener(OnStatusEffectEvent);
        }
        
        private void OnStatusEffectEvent(StatusEffectType effectType, int stackAmount)
        {
            switch (effectType)
            {
                case StatusEffectType.Burn:
                    m_burnNode.UpdateStack(stackAmount);
                    break;
                case StatusEffectType.Poison:
                    m_poisonNode.UpdateStack(stackAmount);
                    break;
                case StatusEffectType.Frozen:
                    break;
                case StatusEffectType.Shock:
                    break;
                case StatusEffectType.Bleed:
                    m_bleedNode.UpdateStack(stackAmount);
                    break;
            }
        }
    }
}

