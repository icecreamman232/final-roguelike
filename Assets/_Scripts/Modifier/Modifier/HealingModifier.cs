using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum HealingModifierType
    {
        FLAT_HEALING,
        PERCENT_MAX_HEALTH,
    }
    
    
    [CreateAssetMenu(fileName = "HealingModifier", menuName = "SGGames/Modifiers/Healing Modifier")]
    public class HealingModifier : Modifier
    {
        public HealingModifierType HealingModifierType;
        public float ChanceToHeal;
        public float ModifierValue;
    }
}