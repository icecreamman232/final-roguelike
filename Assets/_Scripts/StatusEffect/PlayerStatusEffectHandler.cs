
using SGGames.Scripts.Core;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.StatusEffects
{
    public class PlayerStatusEffectHandler : StatusEffectHandler, IPlayerStatusEffectService
    {
        [SerializeField] protected PlayerModifierHandler m_modifierHandler;
        public PlayerModifierHandler ModifierHandler => m_modifierHandler;
        
        protected override void Start()
        {
            m_forPlayer = true;
            base.Start();
            ServiceLocator.RegisterService<PlayerStatusEffectHandler>(this);
        }
    }
}

