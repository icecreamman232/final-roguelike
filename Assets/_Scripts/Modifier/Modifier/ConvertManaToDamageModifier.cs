using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "Convert Mana To Damage Modifier", menuName = "SGGames/Modifiers/Convert Mana To Damage Modifier")]
    public class ConvertManaToDamageModifier : Modifier
    {
        public float ManaToDamageRate;
    }
}


