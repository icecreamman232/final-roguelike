using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class Modifier : ScriptableObject
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

