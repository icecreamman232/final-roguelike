using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerHealthModifierProcessor : ModifierProcessor
    {
        
        public override void StartModifier()
        {
            base.StartModifier();

            var healthModifier = (HealthModifier)m_modifier;
            
            switch (healthModifier.HealthModifierType)
            {
                case HealthModifierType.ModifyPercentDamageTaken:
                    m_handler.PlayerHealth.ModifyPercentDamageTaken(healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ModifyCurrentHPForDuration:
                    m_handler.PlayerHealth.ModifyCurrentHealth(healthModifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.ModifyMaxHPForDuration:
                    m_handler.PlayerHealth.ModifyMaxHealth(healthModifier.ModifierValue,healthModifier.IsPercentValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.SetImmortalForDuration:
                    m_handler.PlayerHealth.SetImmortal(true);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.ModifyCurrentHP:
                    m_handler.PlayerHealth.ModifyCurrentHealth(healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ModifyMaxHP:
                    m_handler.PlayerHealth.ModifyMaxHealth(healthModifier.ModifierValue, healthModifier.IsPercentValue);
                    break;
                case HealthModifierType.ModifyDodgeRate:
                    m_handler.PlayerHealth.AddDodgeRate(healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ChanceToNotTakingDamage:
                    m_handler.PlayerHealth.ModifyChanceToNotTakeDamage(healthModifier.ModifierValue);
                    break;
            }

            m_modifier.IsRunning = true;
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((HealthModifier)m_modifier).HealthModifierType} " +
                      $"- Value:{((HealthModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    
        public override void StopModifier()
        {
            var healthModifier = (HealthModifier)m_modifier;
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{healthModifier.HealthModifierType} " +
                      $"- Value:{healthModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            switch (healthModifier.HealthModifierType)
            {
                case HealthModifierType.ModifyPercentDamageTaken:
                    m_handler.PlayerHealth.ModifyPercentDamageTaken(-healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ModifyCurrentHPForDuration:
                    m_handler.PlayerHealth.ModifyCurrentHealth(-healthModifier.ModifierValue);
                    break;
                case HealthModifierType.SetImmortalForDuration:
                    m_handler.PlayerHealth.SetImmortal(false);
                    break;
                case HealthModifierType.ModifyMaxHP:
                    m_handler.PlayerHealth.ModifyMaxHealth(-healthModifier.ModifierValue, healthModifier.IsPercentValue);
                    break;
                case HealthModifierType.ModifyCurrentHP:
                    m_handler.PlayerHealth.ModifyCurrentHealth(-healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ModifyDodgeRate:
                    m_handler.PlayerHealth.AddDodgeRate(-healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ChanceToNotTakingDamage:
                    m_handler.PlayerHealth.ModifyChanceToNotTakeDamage(-healthModifier.ModifierValue);
                    break;
            }

            base.StopModifier();
        }

        protected override void Update()
        {
            if (!m_isProcessing) return;
            
            m_timer += Time.deltaTime;
            if (m_timer >= m_modifier.Duration)
            {
                m_timer = 0;
                StopModifier();
            }
        }
    }
}

