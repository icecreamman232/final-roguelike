using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Damages
{
    public enum DamageInfluencerType
    {
        ADD_MIN_DAMAGE,
        ADD_MAX_DAMAGE,
        ADD_DAMAGE, //Meaning adding to both min and max damage
        MULTIPLY_DAMAGE,
        CRITICAL_DAMAGE,
        CRITICAL_CHANCE,
        STUNNING,
    }
    
    /// <summary>
    /// Class to represent a damage source that can be caused by chance and can affect on final damage from weapon
    /// </summary>
    [Serializable]
    public class DamageInfluencer
    {
        public DamageInfluencerType InfluencerType;
        public float ChanceToCause;
        public float DamageValue;

        public DamageInfluencer(DamageInfluencerType type, float chance, float value)
        {
            InfluencerType = type;
            ChanceToCause = chance;
            DamageValue = value;
        }
        
        public float GetDamage()
        {
            var chance = Random.Range(0, 101);
            if (chance > ChanceToCause) return 0;
            return DamageValue;
        }
    }
}

