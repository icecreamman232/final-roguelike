using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class Modifier : ScriptableObject
    {
        public ModifierType ModifierType;
        public float Duration;
        public bool CanStack;
        public int MaxStackNumber;
    }

    public enum ModifierType
    {
        MOVEMENT,
        HEALTH,
        DAMAGE,
    }
}

