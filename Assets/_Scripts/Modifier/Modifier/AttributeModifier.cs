
using SGGames.Scripts.Common;
using UnityEngine;


namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "Attribute Modifier", menuName = "SGGames/Modifiers/Attribute", order = 2)]
    public class AttributeModifier : Modifier
    {
        public AttributeType AttributeType;
        public int ModifierValue;
    }
}
