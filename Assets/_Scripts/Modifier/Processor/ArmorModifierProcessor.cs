using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ArmorModifierProcessor : ModifierProcessor
    {
        [SerializeField] private ArmorModifier m_modifier;
        [SerializeField] private PlayerHealth m_playerHealth;

        private float m_timer;
        private ModifierHandler m_handler;
        
        public ArmorModifier Modifier => m_modifier;

        public void Initialize(ModifierHandler modifierHandler,ArmorModifier modifier, PlayerHealth playerHealth)
        {
            m_handler = modifierHandler;
            m_modifier = modifier;
            m_playerHealth = playerHealth;
        }

        public override void StartModifier()
        {
            base.StartModifier();
            switch (m_modifier.ArmorModifierType)
            {
                case ArmorModifierType.IncreaseArmor:
                    m_playerHealth.AddArmor(m_modifier.ModifierValue);
                    break;
                case ArmorModifierType.DecreaseArmor:
                    m_playerHealth.AddArmor(-m_modifier.ModifierValue);
                    break;
                case ArmorModifierType.IncreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case ArmorModifierType.DecreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(-m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.ArmorModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            base.StopModifier();

            switch (m_modifier.ArmorModifierType)
            {
                case ArmorModifierType.IncreaseArmor:
                    m_playerHealth.AddArmor(-m_modifier.ModifierValue);
                    break;
                case ArmorModifierType.DecreaseArmor:
                    m_playerHealth.AddArmor(m_modifier.ModifierValue);
                    break;
                case ArmorModifierType.IncreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(-m_modifier.ModifierValue);
                    break;
                case ArmorModifierType.DecreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(m_modifier.ModifierValue);
                    break;
            }
            
            m_isProcessing = false;
            m_handler.RemoveArmorModifierProcessor(this);
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.ArmorModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}
