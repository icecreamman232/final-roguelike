
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "TriggerAfterGameEventModifier", menuName = "SGGames/Modifiers/Trigger After Game Event Modifier")]
    public class TriggerAfterGameEventModifier : Modifier
    {
        public GameEventType EventTypeToTrigger;
        public Modifier ModifierToBeTriggered;
        public bool TriggerOnce;
    }
}


