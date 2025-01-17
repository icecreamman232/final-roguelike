
using SGGames.Scripts.Core;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.StatusEffects
{
    public class PlayerStatusEffectHandler : StatusEffectHandler, IPlayerStatusEffectService
    {
        private PlayerModifierHandler m_modifierHandler;
        public PlayerModifierHandler ModifierHandler
        {
            get
            {
                if (m_modifierHandler == null)
                {
                    m_modifierHandler = ServiceLocator.GetService<PlayerModifierHandler>();
                }
                return m_modifierHandler;
            }
        }

        protected override void Start()
        {
            m_forPlayer = true;
            base.Start();
            ServiceLocator.RegisterService<PlayerStatusEffectHandler>(this);
        }
    }
}

