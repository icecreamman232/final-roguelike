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
            switch (((HealthModifier)m_modifier).HealthModifierType)
            {
                case HealthModifierType.DecreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(-((HealthModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.IncreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(((HealthModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.OverrideCurrentHP_ForDuration:
                    m_playerHealth.OverrideCurrentHealth(((HealthModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case HealthModifierType.SetImmortal_ForDuration:
                    m_playerHealth.SetImmortal(true);
                    m_isProcessing = true;
                    break;
                
                case HealthModifierType.IncreaseCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.DecreaseCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(-((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.OverrideCurrentHP:
                    m_playerHealth.OverrideCurrentHealth(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.DecreaseMaxHP:
                    m_playerHealth.ModifyMaxHealth(-((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.IncreaseMaxHP:
                    m_playerHealth.ModifyMaxHealth(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.OverrideMaxHP:
                    m_playerHealth.OverrideMaxHealth(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.IncreaseDodge:
                    m_playerHealth.AddDodgeRate(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.ReduceDodge:
                    m_playerHealth.AddDodgeRate(-((HealthModifier)m_modifier).ModifierValue);
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
            base.StopModifier();
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((HealthModifier)m_modifier).HealthModifierType} " +
                      $"- Value:{((HealthModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            switch (((HealthModifier)m_modifier).HealthModifierType)
            {
                case HealthModifierType.DecreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.IncreaseCurrentHP_ForDuration:
                    m_playerHealth.ModifyCurrentHealth(-((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.OverrideCurrentHP_ForDuration:
                    m_playerHealth.ResetHealth();
                    break;
                case HealthModifierType.SetImmortal_ForDuration:
                    m_playerHealth.SetImmortal(false);
                    break;
                case HealthModifierType.DecreaseMaxHP:
                    m_playerHealth.ModifyMaxHealth(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.IncreaseMaxHP:
                    m_playerHealth.ModifyMaxHealth(-((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.OverrideMaxHP:
                    //TODO:Implement a way to save previous value before got override
                    break;
                case HealthModifierType.IncreaseCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(-((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.DecreaseCurrentHP:
                    m_playerHealth.ModifyCurrentHealth(((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.OverrideCurrentHP:
                    //TODO:Implement a way to save previous value before got override
                    break;
                case HealthModifierType.IncreaseDodge:
                    m_playerHealth.AddDodgeRate(-((HealthModifier)m_modifier).ModifierValue);
                    break;
                case HealthModifierType.ReduceDodge:
                    m_playerHealth.AddDodgeRate(((HealthModifier)m_modifier).ModifierValue);
                    break;
            }

            m_modifier.IsRunning = false;
            m_isProcessing = false;
            m_handler.RemoveProcessor(this);
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

