using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerGameEventModifierProcessor : ModifierProcessor
    {
        public override void Initialize(string id, PlayerModifierHandler modifierHandler, Modifier modifier)
        {
            base.Initialize(id, modifierHandler, modifier);
            
            var gameEventModifier = ((GameEventModifier)m_modifier);
            m_handler.RegisterModifier(gameEventModifier.ModifierToBeTriggered);
        }

        public override void StartModifier()
       {
           base.StartModifier();
           m_handler.StartProcessor(((GameEventModifier)m_modifier).ModifierToBeTriggered);
           
           Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                     $"- Type: Trigger {((GameEventModifier)m_modifier).ModifierToBeTriggered} After Event {((GameEventModifier)m_modifier).EventTypeToTrigger}</color> ");
       }
        
        public override void StopModifier()
        {
            Debug.Log($"<color=green>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type: Trigger {((GameEventModifier)m_modifier).ModifierToBeTriggered} After Event {((GameEventModifier)m_modifier).EventTypeToTrigger}</color> ");
            
            //Remove sub modifier
            m_handler.UnregisterModifier(((GameEventModifier)m_modifier).ModifierToBeTriggered);
            
            base.StopModifier();
        }
    }
}

