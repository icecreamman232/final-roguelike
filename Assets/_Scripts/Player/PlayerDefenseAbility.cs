using System;
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
        protected PlayerInputAction m_inputAction;
        
        protected override void Start()
        {
            base.Start();
            m_inputAction = new PlayerInputAction();
            m_inputAction.Enable();
            m_inputAction.Player.DefenseAbility.performed += OnPressDefenseAbilityButton;
        }

        private void OnDestroy()
        {
            m_inputAction.Player.DefenseAbility.performed -= OnPressDefenseAbilityButton;
        }

        protected virtual void OnPressDefenseAbilityButton(InputAction.CallbackContext context)
        {
            
        }

        protected virtual void ResetAbility()
        {
            
        }
    }
}

