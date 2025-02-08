using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum ArmorModifierType
    {
        ModifyArmor,
        ModifyArmorForDuration,
    }
    
    [CreateAssetMenu(fileName = "ArmorModifier", menuName = "SGGames/Modifiers/Armor",order = 0)]
    public class ArmorModifier : Modifier
    {
        public ArmorModifierType ArmorModifierType;
        public float ModifierValue;
    }
}
