using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerManaModifierProcessor : ModifierProcessor
    {
        [SerializeField] private float m_totalModifiedValue;
        
        public override void StartModifier()
        {
            base.StartModifier();
            var manaModifier = ((ManaModifier)m_modifier);
            
            
            switch (manaModifier.ManaModifierType)
            {
                case ManaModifierType.Modify_Max_Mana:
                    m_totalModifiedValue = manaModifier.IsPercentValue 
                        ? m_handler.PlayerMana.MaxMana * manaModifier.ModifierValue
                        : manaModifier.ModifierValue;
                    
                    m_handler.PlayerMana.AddMaxMana(m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Current_Mana:
                    m_totalModifiedValue = manaModifier.IsPercentValue 
                        ? m_handler.PlayerMana.CurrentMana * manaModifier.ModifierValue
                        : manaModifier.ModifierValue;
                    
                    m_handler.PlayerMana.AddCurrentMana(m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Max_Mana_For_Duration:
                    m_totalModifiedValue = manaModifier.IsPercentValue 
                        ? m_handler.PlayerMana.MaxMana * manaModifier.ModifierValue
                        : manaModifier.ModifierValue;
                    
                    
                    m_handler.PlayerMana.AddMaxMana(m_totalModifiedValue);
                    m_isProcessing = true;
                    break;
                case ManaModifierType.Modify_Current_Mana_For_Duration:
                    m_totalModifiedValue = manaModifier.IsPercentValue 
                        ? m_handler.PlayerMana.CurrentMana * manaModifier.ModifierValue
                        : manaModifier.ModifierValue;
                    
                    m_handler.PlayerMana.AddCurrentMana(m_totalModifiedValue);
                    m_isProcessing = true;
                    break;
                case ManaModifierType.Modify_Mana_Regenerate:
                    m_totalModifiedValue = manaModifier.IsPercentValue 
                        ? m_handler.PlayerMana.ManaRegenerateRate * manaModifier.ModifierValue
                        : manaModifier.ModifierValue;
                    
                    m_handler.PlayerMana.AddManaRegeneration(m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Mana_Regenerate_For_Duration:
                    m_totalModifiedValue = manaModifier.IsPercentValue 
                        ? m_handler.PlayerMana.ManaRegenerateRate * manaModifier.ModifierValue
                        : manaModifier.ModifierValue;
                    
                    m_handler.PlayerMana.AddManaRegeneration(m_totalModifiedValue);
                    m_isProcessing = true;
                    break;
            }
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((ManaModifier)m_modifier).ManaModifierType} " +
                      $"- Value:{((ManaModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            var manaModifier = ((ManaModifier)m_modifier);

            switch (manaModifier.ManaModifierType)
            {
                case ManaModifierType.Modify_Max_Mana:
                    m_handler.PlayerMana.AddMaxMana(-m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Current_Mana:
                    m_handler.PlayerMana.AddCurrentMana(-m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Max_Mana_For_Duration:
                    m_handler.PlayerMana.AddMaxMana(-m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Current_Mana_For_Duration:
                    m_handler.PlayerMana.AddCurrentMana(-m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Mana_Regenerate:
                    m_handler.PlayerMana.AddManaRegeneration(-m_totalModifiedValue);
                    break;
                case ManaModifierType.Modify_Mana_Regenerate_For_Duration:
                    m_handler.PlayerMana.AddManaRegeneration(-m_totalModifiedValue);
                    break;
            }
            base.StopModifier();
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((ManaModifier)m_modifier).ManaModifierType} " +
                      $"- Value:{((ManaModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
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
