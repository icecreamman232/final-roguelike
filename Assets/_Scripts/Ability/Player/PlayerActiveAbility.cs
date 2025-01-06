using SGGames.Scripts.Abilities;
using SGGames.Scripts.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Player
{
    /// <summary>
    /// Player's ability that require pressing a button to activate
    /// </summary>
    public class PlayerActiveAbility : Ability
    {
        [SerializeField] protected InputContextEvent m_abilityButtonPressedEvent;
        [SerializeField] protected PlayerEvent m_playerEvent;
        protected override void Start()
        {
            base.Start();
            m_abilityButtonPressedEvent.AddListener(OnPressedAbilityButton);
        }

        protected virtual void OnDestroy()
        {
            m_abilityButtonPressedEvent.RemoveListener(OnPressedAbilityButton);
        }

        protected virtual void OnPressedAbilityButton(InputAction.CallbackContext context)
        {
            
        }
    }
}

