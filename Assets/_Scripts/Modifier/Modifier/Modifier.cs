using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class Modifier : ScriptableObject
    {
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
    }
}

