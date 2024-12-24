using SGGames.Scripts.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Player
{
    public enum PlayerAbilityState
    {
        READY,
        BEFORE_PLAYING,
        PLAYING,
        POST_PLAYING,
        COOLDOWN,
    }
    public class PlayerDefenseAbility : PlayerBehavior
    {
        [SerializeField] protected PlayerAbilityState m_abilityState;
        [SerializeField] protected InputContextEvent m_defenseAbilityButtonPressedEvent;
        
        protected override void Start()
        {
            base.Start();
            m_defenseAbilityButtonPressedEvent.AddListener(OnPressDefenseAbilityButton);
        }

        private void OnDestroy()
        {
            m_defenseAbilityButtonPressedEvent.RemoveListener(OnPressDefenseAbilityButton);
        }

        protected virtual void OnPressDefenseAbilityButton(InputAction.CallbackContext context)
        {
            
        }

        protected virtual void ResetAbility()
        {
            
        }
    }
}

