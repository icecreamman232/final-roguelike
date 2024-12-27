
using SGGames.Scripts.Common;
using UnityEngine;


namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "Attribute Modifier", menuName = "SGGames/Modifiers/Attribute Modifier")]
    public class AttributeModifier : Modifier
    {
        public AttributeType AttributeType;
        public int ModifierValue;
    }
}
