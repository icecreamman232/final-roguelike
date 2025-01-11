using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerEventModifierProcessor : ModifierProcessor
    {
        public override void Initialize(string id, PlayerModifierHandler modifierHandler, Modifier modifier)
        {
            base.Initialize(id, modifierHandler, modifier);
            
            var playerEventModifier = ((PlayerEventModifier)m_modifier);
            m_handler.RegisterModifier(playerEventModifier.ModifierToBeTriggered);
        }


        public override void StartModifier()
        {
            base.StartModifier();
            m_handler.StartProcessor(((PlayerEventModifier)m_modifier).ModifierToBeTriggered);
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type: Trigger {((PlayerEventModifier)m_modifier).ModifierToBeTriggered} After Event {((PlayerEventModifier)m_modifier).EventTypeToTrigger}</color> ");
        }

        public override void StopModifier()
        {
            
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type: Trigger {((PlayerEventModifier)m_modifier).ModifierToBeTriggered} After Event {((PlayerEventModifier)m_modifier).EventTypeToTrigger}</color> ");
            
            //Remove sub modifier
            m_handler.UnregisterModifier(((PlayerEventModifier)m_modifier).ModifierToBeTriggered);
            
            base.StopModifier();
        }
    }  
}

