using SGGames.Scripts.Core;
using SGGames.Scripts.EditorExtensions;
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerHealthConditionModifierProcessor : ModifierProcessor
    {
        [SerializeField] [ReadOnly] private bool m_isTriggered;
        
        public override void Initialize(string id, PlayerModifierHandler modifierHandler, Modifier modifier)
        {
            base.Initialize(id, modifierHandler, modifier);
            var playerHealth = ServiceLocator.GetService<PlayerHealth>();
            playerHealth.OnChangeCurrentHealth += OnChangeCurrentHealth;
        }

        public override void StopModifier()
        {
            base.StopModifier();
            var playerHealth = ServiceLocator.GetService<PlayerHealth>();
            playerHealth.OnChangeCurrentHealth -= OnChangeCurrentHealth;
        }

        private void OnChangeCurrentHealth(float currentHealth)
        {
            var healthConditionMod = m_modifier as HealthConditionModifier;
            if (Compare(healthConditionMod.ComparisonType, m_handler.PlayerHealth.CurrentHealth,
                    healthConditionMod.ThresholdValue * m_handler.PlayerHealth.MaxHealth))
            {
                if (m_isTriggered) return;
                TriggerModifiers();
                m_isTriggered = true;
            }
            else
            {
                if (!m_isTriggered) return;
                RemoveModifiers();
                m_isTriggered = false;
            }
        }

        private void TriggerModifiers()
        {
            var healthConditionMod = m_modifier as HealthConditionModifier;
            foreach (var modifier in healthConditionMod.ToTriggerModifiers)
            {
                m_handler.RegisterModifier(modifier);
            }
        }

        private void RemoveModifiers()
        {
            var healthConditionMod = m_modifier as HealthConditionModifier;
            foreach (var modifier in healthConditionMod.ToTriggerModifiers)
            {
                m_handler.UnregisterModifier(modifier);
            }
        }
    }
}

