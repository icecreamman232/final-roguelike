
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum DamageModifierType
    {
        ModifyDamage,
        ModifyDamageForDuration,
        ModifyMultiplyDamageForDuration,
        
        ModifyCriticalChance,
        ModifyCriticalDamage,
        
        ModifyMultiplyDamage,
        ModifyStun,
    }
    
    [CreateAssetMenu(fileName = "DamageModifier", menuName = "SGGames/Modifiers/Damage Modifier")]
    public class DamageModifier : Modifier
    {
        public DamageModifierType DamageModifierType;
        public float ChanceToCause;
        public float ModifierValue;
    }
}
