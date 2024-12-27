using System;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class AttributeModifierProcessor : ModifierProcessor
    {
        public override void StartModifier()
        {
            base.StartModifier();
            switch (((AttributeModifier)m_modifier).AttributeType)
            {
                case AttributeType.Strength:
                    m_handler.PlayerAttributeController.AddStrength(((AttributeModifier)m_modifier).ModifierValue);
                    break;
                case AttributeType.Agility:
                    m_handler.PlayerAttributeController.AddAgility(((AttributeModifier)m_modifier).ModifierValue);
                    break;
                case AttributeType.Intelligence:
                    m_handler.PlayerAttributeController.AddIntelligence(((AttributeModifier)m_modifier).ModifierValue);
                    break;
            }
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((AttributeModifier)m_modifier).AttributeType} " +
                      $"- Value:{((AttributeModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            base.StopModifier();
            switch (((AttributeModifier)m_modifier).AttributeType)
            {
                case AttributeType.Strength:
                    m_handler.PlayerAttributeController.AddStrength(-((AttributeModifier)m_modifier).ModifierValue);
                    break;
                case AttributeType.Agility:
                    m_handler.PlayerAttributeController.AddAgility(-((AttributeModifier)m_modifier).ModifierValue);
                    break;
                case AttributeType.Intelligence:
                    m_handler.PlayerAttributeController.AddIntelligence(-((AttributeModifier)m_modifier).ModifierValue);
                    break;
            }
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((AttributeModifier)m_modifier).AttributeType} " +
                      $"- Value:{((AttributeModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}

