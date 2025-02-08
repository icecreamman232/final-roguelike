
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "Player Event Modifier", menuName = "SGGames/Modifiers/Player Event",order = 12)]
    public class PlayerEventModifier : Modifier
    {
        public PlayerEventType EventTypeToTrigger;
        public Modifier ModifierToBeTriggered;
        public bool TriggerOnce;
    }
}


