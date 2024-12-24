using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class TriggerAfterEventModifierProcessor : ModifierProcessor
    {
       public override void StartModifier()
       {
           base.StartModifier();
           m_handler.RegisterModifier(((TriggerAfterEventModifier)m_modifier).ModifierToBeTriggered);
           Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                     $"- Type: Trigger {((TriggerAfterEventModifier)m_modifier).ModifierToBeTriggered} After Event {((TriggerAfterEventModifier)m_modifier).EventTypeToTrigger}</color> ");
       }
    }
}

