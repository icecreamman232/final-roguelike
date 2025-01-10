
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "WeaponTypeConditionModifier", menuName = "SGGames/Modifiers/Weapon Type")]
    public class WeaponTypeConditionModifier : Modifier
    {
        public Modifier ModifierForRangeWeapon;
        public Modifier ModifierForMeleeWeapon;
    }
}
