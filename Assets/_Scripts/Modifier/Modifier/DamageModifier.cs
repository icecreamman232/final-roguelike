
using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public enum DamageModifierType
    {
        IncreaseDamage,
        ReduceDamage,
        MultiplyDamage,
        
        IncreaseDamage_ForDuration,
        ReduceDamage_ForDuration,
        MultiplyDamage_ForDuration,
    }
    
    [CreateAssetMenu(fileName = "DamageModifier", menuName = "SGGames/Modifiers/Damage Modifier")]
    public class DamageModifier : Modifier
    {
        public DamageModifierType DamageModifierType;
        public float ModifierValue;
    }
}
