
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.StatusEffects
{
    public class EnemyStatusEffectHandler : StatusEffectHandler
    {
        [SerializeField] protected EnemyModifierHandler m_modifierHandler;
        public EnemyModifierHandler ModifierHandler => m_modifierHandler;
        
        protected override void Start()
        {
            m_forPlayer = false;
            base.Start();
        }
    }
}

