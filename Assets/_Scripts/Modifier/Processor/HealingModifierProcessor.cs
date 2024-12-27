using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class HealingModifierProcessor : ModifierProcessor
    {
        public override void StartModifier()
        {
            base.StartModifier();
            
            var healingModifier = ((HealingModifier)m_modifier);
            switch (healingModifier.HealingModifierType)
            {
                case HealingModifierType.FLAT_HEALING:
                    m_handler.PlayerHealth.HealingFlatAmount(healingModifier.ModifierValue);
                    break;
                case HealingModifierType.PERCENT_MAX_HEALTH:
                    m_handler.PlayerHealth.HealingPercentMaxHealth(healingModifier.ModifierValue);
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{healingModifier.HealingModifierType} " +
                      $"- Value:{healingModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((HealingModifier)m_modifier).HealingModifierType} " +
                      $"- Value:{((HealingModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            base.StopModifier();
        }
    }
}

