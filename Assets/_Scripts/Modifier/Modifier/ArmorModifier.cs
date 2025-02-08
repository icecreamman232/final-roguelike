using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum ArmorModifierType
    {
        ModifyArmor,
        ModifyArmorForDuration,
    }
    
    [CreateAssetMenu(fileName = "ArmorModifier", menuName = "SGGames/Modifiers/Armor")]
    public class ArmorModifier : Modifier
    {
        public ArmorModifierType ArmorModifierType;
        public float ModifierValue;
    }
}
