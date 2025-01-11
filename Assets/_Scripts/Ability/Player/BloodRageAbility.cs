
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class BloodRageAbility : Ability, ISelectableAbility
    {
        [Header("Ability")] 
        [SerializeField] private float m_tickTime;
        [SerializeField] private float m_healthToLosePerTick;
        [SerializeField] private DamageModifier m_damageModifier;
        private PlayerModifierHandler m_modifierHandler;

        private float m_tickTimer;
        
        public void Initialize(PlayerModifierHandler modifierHandler)
        {
            m_modifierHandler = modifierHandler;
            m_modifierHandler.RegisterModifier(m_damageModifier);
            m_abilityState = AbilityState.TRIGGERING;
        }

        protected override void TriggeringState()
        {
            m_tickTimer += Time.deltaTime;
            if (m_tickTimer >= m_tickTime)
            {
                m_tickTimer = 0;
                Tick();
            }
            base.TriggeringState();
        }

        private void Tick()
        {
            //The ability wont kill player
            if (m_modifierHandler.PlayerHealth.CurrentHealth <= m_healthToLosePerTick) return;
            
            m_modifierHandler.PlayerHealth.TakeDamage(m_healthToLosePerTick,this.gameObject,0f);
        }
    }
}
