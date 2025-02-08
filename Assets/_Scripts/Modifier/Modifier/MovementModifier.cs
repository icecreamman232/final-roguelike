using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum MovementModifierType
    {
        ModifyMovespeedForDuration,
        ModifyMovespeed,
    }
    
    [CreateAssetMenu(fileName = "MovementModifier", menuName = "SGGames/Modifiers/Movement",order = 11)]
    public class MovementModifier : Modifier
    {
        public MovementModifierType MovementModifierType;
        public float ModifierValue;
    }
}
