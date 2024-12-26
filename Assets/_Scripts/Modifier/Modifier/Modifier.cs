using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class Modifier : ScriptableObject
    {
        [TextArea]
        public string Description;
        [Header("Data")]
        public ModifierType ModifierType;
        public float Duration;
        public bool InstantTrigger;
        [Header("Runtime Data")] 
        public bool IsRunning;
    }

    public enum ModifierType
    {
        MOVEMENT,
        HEALTH,
        DAMAGE,
        TRIGGER_AFTER_GAME_EVENT,
        ARMOR,
        COIN,
        TRIGGER_AFTER_PLAYER_EVENT,
        HEALING,
    }
}

