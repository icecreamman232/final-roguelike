using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class HealthModifierProcessor : ModifierProcessor
    {
        [SerializeField] private PlayerHealth m_playerHealth;
        
        public override void Initialize(string id, ModifierHandler handler, Modifier modifier)
        {
            base.Initialize(id, handler, modifier);
            m_playerHealth = handler.PlayerHealth;
        }
        
        public override void StartModifier()
        {
            base.StartModifier();

            var healthModifier = (HealthModifier)m_modifier;
            
            switch (healthModifier.HealthModifierType)
            {
                case HealthModifierType.ModifyCurrentHPForDuration:
                    m_playerHealth.ModifyCurrentHealth(healthModifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.ModifyMaxHPForDuration:
                    m_playerHealth.ModifyMaxHealth(healthModifier.ModifierValue,healthModifier.IsPercentValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.SetImmortal_ForDuration:
                    m_playerHealth.SetImmortal(true);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.ModifyCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ModifyMaxHP:
                    m_playerHealth.ModifyMaxHealth(healthModifier.ModifierValue, healthModifier.IsPercentValue);
                    break;
                case HealthModifierType.ModifyDodgeRate:
                    m_playerHealth.AddDodgeRate(healthModifier.ModifierValue);
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
                case HealthModifierType.ModifyCurrentHPForDuration:
                    m_playerHealth.ModifyCurrentHealth(-healthModifier.ModifierValue);
                    break;
                case HealthModifierType.SetImmortal_ForDuration:
                    m_playerHealth.SetImmortal(false);
                    break;
                case HealthModifierType.ModifyMaxHP:
                    m_playerHealth.ModifyMaxHealth(-healthModifier.ModifierValue, healthModifier.IsPercentValue);
                    break;
                case HealthModifierType.ModifyCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(-healthModifier.ModifierValue);
                    break;
                case HealthModifierType.ModifyDodgeRate:
                    m_playerHealth.AddDodgeRate(-healthModifier.ModifierValue);
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

