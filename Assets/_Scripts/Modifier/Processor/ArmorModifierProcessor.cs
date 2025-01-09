using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ArmorModifierProcessor : ModifierProcessor
    {
        public override void StartModifier()
        {
            var armorModifier = (ArmorModifier)m_modifier;
            
            base.StartModifier();
            switch (armorModifier.ArmorModifierType)
            {
                case ArmorModifierType.ModifyArmor:
                    m_handler.PlayerHealth.AddArmor(armorModifier.ModifierValue);
                    break;
                case ArmorModifierType.ModifyArmorForDuration:
                    m_handler.PlayerHealth.AddArmor(armorModifier.ModifierValue);
                    m_isProcessing = true;
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{armorModifier.ArmorModifierType} " +
                      $"- Value:{armorModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            var armorModifier = (ArmorModifier)m_modifier;
            
            switch (armorModifier.ArmorModifierType)
            {
                case ArmorModifierType.ModifyArmor:
                    m_handler.PlayerHealth.AddArmor(-armorModifier.ModifierValue);
                    break;
                case ArmorModifierType.ModifyArmorForDuration:
                    m_handler.PlayerHealth.AddArmor(-armorModifier.ModifierValue);
                    break;
            }
            
            base.StopModifier();
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{armorModifier.ArmorModifierType} " +
                      $"- Value:{armorModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}
