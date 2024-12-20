using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class Modifier : ScriptableObject
    {
        public string Description;
        [Header("Data")]
        public ModifierType ModifierType;
        public float Duration;
        [Header("Runtime Data")] 
        public bool IsRunning;
    }

    public enum ModifierType
    {
        MOVEMENT,
        HEALTH,
        DAMAGE,
        TRIGGER_AFTER_GAME_EVENT
    }
}

