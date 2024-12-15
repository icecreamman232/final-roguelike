using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class HealthModifierProcessor : ModifierProcessor
    {
        [SerializeField] private HealthModifier m_modifier;
        [SerializeField] private PlayerHealth m_playerHealth;
        
        private float m_timer;
        private ModifierHandler m_handler;
        
        public void Initialize(ModifierHandler handler, PlayerHealth playerHealth, HealthModifier modifier)
        {
            m_handler = handler;
            m_playerHealth = playerHealth;
            m_modifier = modifier;
        }
        
        public override void StartModifier()
        {
            base.StartModifier();
            switch (m_modifier.HealthModifierType)
            {
                case HealthModifierType.DecreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(-m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.IncreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.OverrideCurrentHP_ForDuration:
                    m_playerHealth.OverrideCurrentHealth(m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.SetImmortal_ForDuration:
                    m_playerHealth.SetImmortal(true);
                    m_isProcessing = true;
                    break;
                
                case HealthModifierType.IncreaseCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(m_modifier.ModifierValue);
                    break;
                case HealthModifierType.DecreaseCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(-m_modifier.ModifierValue);
                    break;
                case HealthModifierType.OverrideCurrentHP:
                    m_playerHealth.OverrideCurrentHealth(m_modifier.ModifierValue);
                    break;
                case HealthModifierType.DecreaseMaxHP:
                    m_playerHealth.ModifyMaxHealth(-m_modifier.ModifierValue);
                    break;
                case HealthModifierType.IncreaseMaxHP:
                    m_playerHealth.ModifyMaxHealth(m_modifier.ModifierValue);
                    break;
                case HealthModifierType.OverrideMaxHP:
                    m_playerHealth.OverrideMaxHealth(m_modifier.ModifierValue);
                    break;
            }

            m_modifier.IsRunning = true;
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.HealthModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    
        public override void StopModifier()
        {
            base.StopModifier();
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.HealthModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            switch (m_modifier.HealthModifierType)
            {
                case HealthModifierType.DecreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(m_modifier.ModifierValue);
                    break;
                case HealthModifierType.IncreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(-m_modifier.ModifierValue);
                    break;
                case HealthModifierType.OverrideCurrentHP_ForDuration:
                    m_playerHealth.ResetHealth();
                    break;
                case HealthModifierType.SetImmortal_ForDuration:
                    m_playerHealth.SetImmortal(false);
                    break;
            }

            m_modifier.IsRunning = false;
            m_isProcessing = false;
            m_handler.RemoveHealthModifierProcessor(this);
        }

        protected override void Update()
        {
            if (!m_isProcessing) return;
            
            m_timer += Time.deltaTime;
            if (m_timer >= m_modifier.Duration)
            {
                StopModifier();
            }
        }
    }
}

