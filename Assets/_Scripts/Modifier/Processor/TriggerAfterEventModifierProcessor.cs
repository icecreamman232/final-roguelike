using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class TriggerAfterEventModifierProcessor : ModifierProcessor
    {
       public override void StartModifier()
       {
           base.StartModifier();
           m_handler.RegisterModifier(((TriggerAfterGameEventModifier)m_modifier).ModifierToBeTriggered);
           Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                     $"- Type: Trigger {((TriggerAfterGameEventModifier)m_modifier).ModifierToBeTriggered} After Event {((TriggerAfterGameEventModifier)m_modifier).EventTypeToTrigger}</color> ");
       }
    }
}

