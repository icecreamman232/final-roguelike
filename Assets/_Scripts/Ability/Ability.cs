using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public enum AbilityState
    {
        READY,
        PRE_TRIGGER,
        TRIGGERING,
        POST_TRIGGER,
        COOLDOWN,
    }

    /// <summary>
    /// Ability that player can choose when leveling up
    /// </summary>
    public interface ISelectableAbility
    {
        public void Initialize(ModifierHandler modifierHandler);
    }

    public class Ability : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] protected AbilityState m_abilityState = AbilityState.READY;
        [SerializeField] [Min(0)] protected float m_coolDown;

        protected float m_cooldownTimer;
        
        public AbilityState CurrentState => m_abilityState;

        protected virtual void Start()
        {
            
        }
        
        protected virtual void Update()
        {
            UpdateState();
        }
        
        protected virtual void UpdateState()
        {
            switch (m_abilityState)
            {
                case AbilityState.READY:
                    ReadyState();
                    break;
                case AbilityState.PRE_TRIGGER:
                    PreTriggerState();
                    break;
                case AbilityState.TRIGGERING:
                    TriggeringState();
                    break;
                case AbilityState.POST_TRIGGER:
                    PostTriggerState();
                    break;
                case AbilityState.COOLDOWN:
                    CooldownState();
                    break;
            }
        }

        protected virtual void ReadyState()
        {

        }

        protected virtual void PreTriggerState()
        {
            
        }

        protected virtual void TriggeringState()
        {

        }

        protected virtual void PostTriggerState()
        {
            StartCooldown();
            m_abilityState = AbilityState.COOLDOWN;
        }

        protected virtual void CooldownState()
        {
            m_cooldownTimer -= Time.deltaTime;
            if (m_cooldownTimer <= 0)
            {
                StopCooldown();
                m_abilityState = AbilityState.READY;
            }
        }
        
        protected virtual void StartCooldown()
        {
            m_cooldownTimer = m_coolDown;
        }

        protected virtual void StopCooldown()
        {
            m_cooldownTimer = 0;
        }
        
        protected virtual void ResetAbility()
        {
            
        }
        
        public virtual void StartAbility()
        {
            m_abilityState = AbilityState.PRE_TRIGGER;
        }

        public virtual void StopAbility()
        {
            m_abilityState = AbilityState.POST_TRIGGER;
        }
    }
}
