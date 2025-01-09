using System;
using System.Collections;
using SGGames.Scripts.Abilities;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.Modifiers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Player
{
    public class KnightShieldAbility : PlayerActiveAbility
    {
        [Header("Shield Params")]
        [SerializeField] private float m_manaCost;
        [SerializeField] private float m_shieldDuration;
        [SerializeField][ReadOnly] private float m_durationTimer;
        [Header("Component Refs")]
        [SerializeField] private CircleCollider2D m_shieldCollider;
        [SerializeField] private ModifierHandler m_modifierHandler;
        [SerializeField] private HealthModifier m_immortalModifier;
        [SerializeField] private MovementModifier m_reduceMovespeedModifier;
        [SerializeField] private AbilityCoolDownEvent m_abilityCoolDownEvent;
        [Header("Animation")]
        [SerializeField] private Animator m_shieldVFXAnimator;
        [SerializeField] private float m_showShieldAnimDuration;
        
        private PlayerMovement m_playerMovement;
        private PlayerMana m_playerMana;
        private readonly int m_showShieldAnimParam = Animator.StringToHash("Trigger_Shield");
        
        protected override void Start()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
            m_playerMana = ServiceLocator.GetService<PlayerMana>();
            m_shieldCollider.enabled = false;
            base.Start();
        }

        protected override void TriggeringState()
        {
            m_durationTimer -= Time.deltaTime;
            if (m_durationTimer <= 0)
            {
                ResetAbility();
            }
            base.TriggeringState();
        }

        protected override void CooldownState()
        {
            base.CooldownState();
            m_abilityCoolDownEvent.Raise(m_cooldownTimer,m_coolDown);
        }
        
        protected override void OnPressedAbilityButton(InputAction.CallbackContext context)
        {
            if (m_abilityState != AbilityState.READY) return;
            if (m_playerMana.CurrentMana < m_manaCost) return;
            
            m_playerEvent.Raise(PlayerEventType.USE_DEFENSE_ABILITY);
            m_playerMana.SpentMana(m_manaCost);
            
            StartCoroutine(OnTriggerShield());
            base.OnPressedAbilityButton(context);
        }

        private IEnumerator OnTriggerShield()
        {
            m_abilityState = AbilityState.PRE_TRIGGER;
            m_playerMovement.ToggleMovement(false);
            m_shieldVFXAnimator.SetBool(m_showShieldAnimParam, true);
            yield return new WaitForSeconds(m_showShieldAnimDuration);
            m_shieldCollider.enabled = true;
            m_playerMovement.ToggleMovement(true);
            
            m_modifierHandler.RegisterModifier(m_immortalModifier);
            m_modifierHandler.RegisterModifier(m_reduceMovespeedModifier);
            
            m_durationTimer = m_shieldDuration;
            
            m_abilityState = AbilityState.TRIGGERING;
        }

        private IEnumerator OnStopShield()
        {
            m_durationTimer = 0;
            m_shieldVFXAnimator.SetBool(m_showShieldAnimParam,false);
            m_shieldCollider.enabled = false;
            yield return new WaitForSeconds(m_showShieldAnimDuration);
            m_abilityState = AbilityState.POST_TRIGGER;
        }

        protected override void ResetAbility()
        {
            StartCoroutine(OnStopShield());
            base.ResetAbility();
        }
    }
}

