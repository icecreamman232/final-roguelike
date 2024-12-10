using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public enum MovementModifierType
    {
        ReduceMS,
        IncreaseMS,
        OverrideSpeed,
    }
    
    [CreateAssetMenu(fileName = "MovementModifier", menuName = "SGGames/Modifiers/Movement Modifier")]
    public class MovementModifier : Modifier
    {
        public MovementModifierType MovementModifierType;
        public float ModifierValue;
    }
}
