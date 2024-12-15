
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "TriggerAfterEventModifier", menuName = "SGGames/Modifiers/Trigger After Event Modifier")]
    public class TriggerAfterEventModifier : Modifier
    {
        public GameEventType EventTypeToTrigger;
        public Modifier ModifierToBeTriggered;
        public bool TriggerOnce;
    }
}


