using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public enum MovementModifierType
    {
        //Temporary type
        ReduceMSForDuration,
        IncreaseMSForDuration,
        OverrideSpeedForDuration,
        
        //Permanent type
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
