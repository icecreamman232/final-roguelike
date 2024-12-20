using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum CoinModifierType
    {
        ADD_EXTRA_COIN_FOR_ENEMY,
        ADD_EXTRA_COIN_FOR_CHEST,
        ADD_EXTRA_COIN_FOR_ALL,
    }
    
    [CreateAssetMenu(fileName = "CoinModifier", menuName = "SGGames/Modifiers/Coin Modifier")]
    public class CoinModifier : Modifier
    {
        public CoinModifierType CoinModifierType;
        public int ModifierValue;
    }
}

