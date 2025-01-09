
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum HealthModifierType
    {
        ModifyPercentDamageTaken,
        ModifyCurrentHPForDuration,
        ModifyMaxHPForDuration,
        ModifyMaxHP,
        ModifyCurrentHP,
        SetImmortal_ForDuration,
        ModifyDodgeRate,
        ChanceToNotTakingDamage,
    }
    
    
    [CreateAssetMenu(fileName = "HealthModifier", menuName = "SGGames/Modifiers/Health Modifier")]
    public class HealthModifier : Modifier
    {
        public HealthModifierType HealthModifierType;
        public float ModifierValue;
        public bool IsPercentValue;
    }
}

