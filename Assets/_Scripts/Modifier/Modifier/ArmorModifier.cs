using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum ArmorModifierType
    {
        IncreaseArmor,
        DecreaseArmor,
        
        IncreaseArmor_ForDuration,
        DecreaseArmor_ForDuration,
    }
    
    [CreateAssetMenu(fileName = "ArmorModifier", menuName = "SGGames/Modifiers/Armor Modifier")]
    public class ArmorModifier : Modifier
    {
        public ArmorModifierType ArmorModifierType;
        public float ModifierValue;
    }
}
