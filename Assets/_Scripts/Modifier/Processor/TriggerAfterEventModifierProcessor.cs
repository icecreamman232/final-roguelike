using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class TriggerAfterEventModifierProcessor : ModifierProcessor
    {
       [SerializeField] private TriggerAfterEventModifier m_modifier;

       private ModifierHandler m_handler;
       
       public TriggerAfterEventModifier Modifier => m_modifier;
       
       public void Initialize(ModifierHandler handler, TriggerAfterEventModifier modifier)
       {
           m_handler = handler;
           m_modifier = modifier;
       }

       public override void StartModifier()
       {
           base.StartModifier();
           m_handler.RegisterModifier(m_modifier.ModifierToBeTriggered);
           Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                     $"- Type: Trigger {m_modifier.ModifierToBeTriggered} After Event {m_modifier.EventTypeToTrigger}</color> ");
       }
    }
}

