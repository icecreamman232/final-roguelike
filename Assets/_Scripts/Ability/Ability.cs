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

    public class Ability : MonoBehaviour, ICooldown
    {
        [SerializeField] protected AbilityState m_abilityState = AbilityState.READY;
        [SerializeField] protected float m_coolDown;

        protected float m_cooldownTimer;
        
        public AbilityState CurrentState => m_abilityState;

        public void StartCooldown()
        {
            m_cooldownTimer = m_coolDown;
        }

        public void StopCooldown()
        {
            m_cooldownTimer = 0;
        }

        public virtual void Update()
        {
            UpdateState();
        }

        public virtual void StartAbility()
        {
            m_abilityState = AbilityState.PRE_TRIGGER;
        }

        public virtual void StopAbility()
        {
            m_abilityState = AbilityState.POST_TRIGGER;
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
            m_abilityState = AbilityState.TRIGGERING;
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
            }
        }
    }
}
