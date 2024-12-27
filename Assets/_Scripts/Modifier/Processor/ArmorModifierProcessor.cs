using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ArmorModifierProcessor : ModifierProcessor
    {
        [SerializeField] private PlayerHealth m_playerHealth;

        public override void Initialize(string id, ModifierHandler modifierHandler,Modifier modifier)
        {
            base.Initialize(id, modifierHandler, modifier);
            m_playerHealth = modifierHandler.PlayerHealth;
        }

        public override void StartModifier()
        {
            base.StartModifier();
            switch (((ArmorModifier)m_modifier).ArmorModifierType)
            {
                case ArmorModifierType.IncreaseArmor:
                    m_playerHealth.AddArmor(((ArmorModifier)m_modifier).ModifierValue);
                    break;
                case ArmorModifierType.DecreaseArmor:
                    m_playerHealth.AddArmor(-((ArmorModifier)m_modifier).ModifierValue);
                    break;
                case ArmorModifierType.IncreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(((ArmorModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case ArmorModifierType.DecreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(-((ArmorModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((ArmorModifier)m_modifier).ArmorModifierType} " +
                      $"- Value:{((ArmorModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            switch (((ArmorModifier)m_modifier).ArmorModifierType)
            {
                case ArmorModifierType.IncreaseArmor:
                    m_playerHealth.AddArmor(-((ArmorModifier)m_modifier).ModifierValue);
                    break;
                case ArmorModifierType.DecreaseArmor:
                    m_playerHealth.AddArmor(((ArmorModifier)m_modifier).ModifierValue);
                    break;
                case ArmorModifierType.IncreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(-((ArmorModifier)m_modifier).ModifierValue);
                    break;
                case ArmorModifierType.DecreaseArmor_ForDuration:
                    m_playerHealth.AddArmor(((ArmorModifier)m_modifier).ModifierValue);
                    break;
            }
            
            base.StopModifier();
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((ArmorModifier)m_modifier).ArmorModifierType} " +
                      $"- Value:{((ArmorModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}
