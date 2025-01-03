using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Manager;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class AttributeRewardController : Singleton<AttributeRewardController>
    {
        [Header("Progression")]
        [SerializeField] private AttributeRarityProgressionData m_attributeTierProgressionData;

        public static int CommonRewardPoint = 1;
        public static int UncommonRewardPoint = 2;
        public static int RareRewardPoint = 3;
        public static int LegendaryRewardPoint = 5;

        private AttributeType GetRandomType(float chance)
        {
            if (chance >= 0 && chance <= 30f)
            {
                return AttributeType.Strength;
            }
            if (chance > 30 && chance <= 60f)
            {
                return AttributeType.Agility;
            }
            return AttributeType.Intelligence;
        }

        public (AttributeTier rate, AttributeType type) GetAttributeReward()
        {
            var rateDropChance = UnityEngine.Random.Range(0f, 100f);
            var typeDropChance = UnityEngine.Random.Range(0f, 90f); //0-90 so we have 30% for each str, agi and intel
            var randomRate = m_attributeTierProgressionData
                .GetAttributeTierAtLevel(InGameProgressManager.Instance.CurrentLevel, rateDropChance);
            var randomType = GetRandomType(typeDropChance);
            
            Debug.Log($"<color=orange>Get att. tier {randomRate} at level:{InGameProgressManager.Instance.CurrentLevel} with chance:{rateDropChance}</color>");
            
            return (randomRate, randomType);
        }
    } 
}

