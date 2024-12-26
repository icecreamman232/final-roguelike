
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "Player Event Modifier", menuName = "SGGames/Modifiers/Player Event Modifier")]
    public class PlayerEventModifier : Modifier
    {
        public PlayerEventType EventTypeToTrigger;
        public Modifier ModifierToBeTriggered;
        public bool TriggerOnce;
    }
}


