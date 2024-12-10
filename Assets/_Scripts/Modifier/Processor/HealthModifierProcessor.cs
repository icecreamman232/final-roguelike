
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Modifier
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
        
        public void StartModifier()
        {
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
            
            Debug.Log("<color=orange>Start Health Modifier </color>");
        }
    
        public void StopModifier()
        {
            Debug.Log("<color=orange>Stop Health Modifier </color>");
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

