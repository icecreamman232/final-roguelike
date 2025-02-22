using SGGames.Scripts.Modifiers;
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.StatusEffects
{
    public class EnemyStatusEffectHandler : StatusEffectHandler
    {
        [SerializeField] protected EnemyModifierHandler m_modifierHandler;
        [SerializeField] protected EnemyStatusBar m_statusBar;
        public EnemyModifierHandler ModifierHandler => m_modifierHandler;
        
        protected override void Start()
        {
            m_forPlayer = false;
            base.Start();
#if UNITY_EDITOR
            if (m_statusBar == null)
            {
                Debug.LogError($"Enemy status bar not found on {this.gameObject}");
            }
#endif
        }

        public override void UpdateStatusUI(StatusEffectType effectType, int stackAmount)
        {
            m_statusBar.OnReceiveStatusEffect(effectType, stackAmount);
            base.UpdateStatusUI(effectType, stackAmount);
        }
    }
}

