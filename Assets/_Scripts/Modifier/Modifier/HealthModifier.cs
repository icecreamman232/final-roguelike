
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum HealthModifierType
    {
        IncreaseCurrentHP_ForDuration,
        DecreaseCurrentHP_ForDuration,
        OverrideCurrentHP_ForDuration,
        
        IncreaseMaxHP,
        DecreaseMaxHP,
        OverrideMaxHP,
        
        IncreaseCurrentHP,
        DecreaseCurrentHP,
        OverrideCurrentHP,
        
        SetImmortal_ForDuration,

        IncreaseDodge,
        ReduceDodge,
    }
    
    
    [CreateAssetMenu(fileName = "HealthModifier", menuName = "SGGames/Modifiers/Health Modifier")]
    public class HealthModifier : Modifier
    {
        public HealthModifierType HealthModifierType;
        public float ModifierValue;
        public bool IsPercentValue;
    }
}

