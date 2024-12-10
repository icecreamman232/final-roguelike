using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class ModifierInfo : ScriptableObject
    {
        public ModifierType ModifierType;
        public float Duration;
    }

    public enum ModifierType
    {
        MOVEMENT,
        HEALTH,
        DAMAGE,
    }
}

