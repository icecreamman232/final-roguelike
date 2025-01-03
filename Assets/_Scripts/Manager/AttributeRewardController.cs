using System;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    [Serializable]
    public class AttributeChance
    {
        public AttributeTier Rate;
        public float Weight;
        [SerializeField] [ReadOnly] private float m_lowChance;
        [SerializeField] [ReadOnly] private float m_upperChance;
        
        public float LowerChance => m_lowChance;
        public float UpperChance => m_upperChance;
        
        public void SetLowerChance(float value)
        {
            m_lowChance = value;
        }

        public void SetUpperChance(float value)
        {
            m_upperChance = value;
        }
    }
    public class AttributeRewardController : Singleton<AttributeRewardController>
    {
        [Header("Weights")]
        [SerializeField] private AttributeChance[] m_dropChanceList;


        public static int CommonRewardPoint = 1;
        public static int UncommonRewardPoint = 2;
        public static int RareRewardPoint = 3;
        public static int LegendaryRewardPoint = 5;
        
        private void Start()
        {
            ComputeDropChance();
        }

        [ContextMenu("Compute Drop Chance")]
        private void ComputeDropChance()
        {
            var totalWeight = 0f;
            var curChance = 0f;
            
            foreach (var reward in m_dropChanceList)
            {
                totalWeight += reward.Weight;
            }
            
            for (int i = 0; i < m_dropChanceList.Length; i++)
            {
                m_dropChanceList[i].SetLowerChance(curChance);
                curChance += m_dropChanceList[i].Weight/totalWeight * 100f;
                m_dropChanceList[i].SetUpperChance(curChance);
            }
        }

        private AttributeTier GetRandomRate(float chance)
        {
            for (int i = 0; i < m_dropChanceList.Length; i++)
            {
                if (m_dropChanceList[i].LowerChance <= chance
                    && m_dropChanceList[i].UpperChance >= chance)
                {
                    return m_dropChanceList[i].Rate;
                }
            }
            return m_dropChanceList[0].Rate;
        }

        private AttributeType GetRandomType(float chance)
        {
            if (chance >= 0 && chance <= 30f)
            {
                return AttributeType.Strength;
            }
            else if (chance > 30 && chance <= 60f)
            {
                return AttributeType.Agility;
            }
            else
            {
                return AttributeType.Intelligence;
            }
        }

        public (AttributeTier rate, AttributeType type) GetAttributeReward()
        {
            var rateDropChance = UnityEngine.Random.Range(0f, 100f);
            var typeDropChance = UnityEngine.Random.Range(0f, 90f); //0-90 so we have 30% for each str, agi and intel
            var randomRate = GetRandomRate(rateDropChance);
            var randomType = GetRandomType(typeDropChance);
            return (randomRate, randomType);
        }
    } 
}

