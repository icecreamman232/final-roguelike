
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum HealthModifierType
    {
        ModifyCurrentHPForDuration,
        ModifyMaxHPForDuration,
        ModifyMaxHP,
        ModifyCurrentHP,
        SetImmortal_ForDuration,
        ModifyDodgeRate,
    }
    
    
    [CreateAssetMenu(fileName = "HealthModifier", menuName = "SGGames/Modifiers/Health Modifier")]
    public class HealthModifier : Modifier
    {
        public HealthModifierType HealthModifierType;
        public float ModifierValue;
        public bool IsPercentValue;
    }
}

