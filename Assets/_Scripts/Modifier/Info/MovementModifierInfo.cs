using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public enum MovementModifierType
    {
        ReduceMS,
        IncreaseMS,
        OverrideSpeed,
    }
    
    [CreateAssetMenu(fileName = "MovementModifierInfo", menuName = "SGGames/Modifiers/Movement Modifier")]
    public class MovementModifierInfo : ModifierInfo
    {
        public MovementModifierType MovementModifierType;
        public float ModifierValue;
    }
}
