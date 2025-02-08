using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    [CreateAssetMenu(fileName = "Attack Time Modifier", menuName = "SGGames/Modifiers/Attack Time", order = 1)]
    public class AttackTimeModifier : Modifier
    {
        public float ModifierValue;
    }
}
