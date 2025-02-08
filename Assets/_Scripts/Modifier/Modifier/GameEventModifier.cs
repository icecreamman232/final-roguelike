
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "TriggerAfterGameEventModifier", menuName = "SGGames/Modifiers/Trigger After Game Event",order = 6)]
    public class GameEventModifier : Modifier
    {
        public GameEventType EventTypeToTrigger;
        public Modifier ModifierToBeTriggered;
        public bool TriggerOnce;
    }
}


