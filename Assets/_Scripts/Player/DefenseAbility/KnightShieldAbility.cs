using System;
using System.Collections;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.Modifiers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Player
{
    public class KnightShieldAbility : PlayerDefenseAbility
    {
        [SerializeField] private float m_shieldDuration;
        [SerializeField] private float m_shieldCooldown;
        [SerializeField][ReadOnly] private float m_durationTimer;
        [SerializeField][ReadOnly] private float m_cooldownTimer;
        [SerializeField] private CircleCollider2D m_shieldCollider;
        [SerializeField] private ModifierHandler m_modifierHandler;
        [SerializeField] private HealthModifier m_immortalModifier;
        [SerializeField] private MovementModifier m_reduceMovespeedModifier;
        [SerializeField] private AbilityCoolDownEvent m_abilityCoolDownEvent;
        [Header("Animation")]
        [SerializeField] private Animator m_shieldVFXAnimator;
        [SerializeField] private float m_showShieldAnimDuration;
        
        private PlayerMovement m_playerMovement;
        private readonly int m_showShieldAnimParam = Animator.StringToHash("Trigger_Shield");
        
        
        protected override void Start()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
            m_shieldCollider.enabled = false;
            base.Start();
        }

        protected override void Update()
        {
            if (!m_isAllow) return;

            switch (m_abilityState)
            {
                case PlayerAbilityState.READY:
                    break;
                case PlayerAbilityState.BEFORE_PLAYING:
                    
                    break;
                case PlayerAbilityState.PLAYING:
                    m_durationTimer -= Time.deltaTime;
                    if (m_durationTimer <= 0)
                    {
                        ResetAbility();
                    }
                    break;
                case PlayerAbilityState.POST_PLAYING:
                    break;
                case PlayerAbilityState.COOLDOWN:
                    m_cooldownTimer -= Time.deltaTime;
                    if (m_cooldownTimer <= 0)
                    {
                        m_cooldownTimer = 0;
                        m_abilityState = PlayerAbilityState.READY;
                    }
                    m_abilityCoolDownEvent.Raise(m_cooldownTimer,m_shieldCooldown);
                    break;
            }
        }

        protected override void OnPressDefenseAbilityButton(InputAction.CallbackContext context)
        {
            if (m_abilityState != PlayerAbilityState.READY) return;
            m_playerEvent.Raise(PlayerEventType.USE_DEFENSE_ABILITY);
            StartCoroutine(OnTriggerShield());
            base.OnPressDefenseAbilityButton(context);
        }

        private IEnumerator OnTriggerShield()
        {
            m_abilityState = PlayerAbilityState.BEFORE_PLAYING;
            m_playerMovement.ToggleMovement(false);
            m_shieldVFXAnimator.SetBool(m_showShieldAnimParam, true);
            yield return new WaitForSeconds(m_showShieldAnimDuration);
            m_abilityState = PlayerAbilityState.PLAYING;
            m_shieldCollider.enabled = true;
            m_playerMovement.ToggleMovement(true);
            
            m_modifierHandler.RegisterModifier(m_immortalModifier);
            m_modifierHandler.RegisterModifier(m_reduceMovespeedModifier);
            
            m_durationTimer = m_shieldDuration;
        }

        private IEnumerator OnStopShield()
        {
            m_abilityState = PlayerAbilityState.POST_PLAYING;
            m_durationTimer = 0;
            m_shieldVFXAnimator.SetBool(m_showShieldAnimParam,false);
            m_shieldCollider.enabled = false;
            yield return new WaitForSeconds(m_showShieldAnimDuration);
            
            m_cooldownTimer = m_shieldCooldown;
            m_abilityState = PlayerAbilityState.COOLDOWN;
        }

        protected override void ResetAbility()
        {
            StartCoroutine(OnStopShield());
            base.ResetAbility();
        }
    }
}

