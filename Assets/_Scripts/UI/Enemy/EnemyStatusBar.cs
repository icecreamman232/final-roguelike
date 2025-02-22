using SGGames.Scripts.StatusEffects;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class EnemyStatusBar : MonoBehaviour
    {
        [SerializeField] private StatusEffectUINode m_burnNode;
        [SerializeField] private StatusEffectUINode m_poisonNode;
        [SerializeField] private StatusEffectUINode m_bleedNode;
        
        private void Start()
        {
            m_burnNode.Hide();
            m_poisonNode.Hide();
            m_bleedNode.Hide();
        }
        
        public void OnReceiveStatusEffect(StatusEffectType type, int stackAmount)
        {
            switch (type)
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

