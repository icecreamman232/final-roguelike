using System;
using SGGames.Scripts.Attribute;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "DropsTable", menuName = "SGGames/Data/Drops Table")]
    public class DropsTableData : ScriptableObject
    {
        [Header("Coins")]
        [SerializeField] private int m_minCoinDrop;
        [SerializeField] private int m_maxCoinDrop;
        [SerializeField] private GameObject m_coinPrefab;
        
        [Header("Keys")]
        [SerializeField] private float m_chanceToDropKey;
        [SerializeField] private int m_minKeyDrop;
        [SerializeField] private int m_maxKeyDrop;
        [SerializeField] private GameObject m_keyPrefab;
        
        [Header("Small EXP")]
        [SerializeField] private int m_minSmallExpDrop;
        [SerializeField] private int m_maxSmallExpDrop;
        [SerializeField] private GameObject m_smallExpPrefab;
        
        [Header("Big EXP")]
        [SerializeField] private float m_chanceToDropBigExp;
        [SerializeField] private int m_minBigExpDrop;
        [SerializeField] private int m_maxBigExpDrop;
        [SerializeField] private GameObject m_bigExpPrefab;
        
        [Header("Drops")]
        [SerializeField] private LootData[] m_dropsList;

        private void OnEnable()
        {
            ComputeItemDropChance();
        }

        [ContextMenu("Compute Item Drop Chance")]
        private void ComputeItemDropChance()
        {
            if (m_dropsList.Length == 0) return;
            var currentChance = 0f;
            var totalWeight = 0f;
            
            for (int i = 0; i < m_dropsList.Length; i++)
            {
                totalWeight += m_dropsList[i].Weight;
            }

            for (int i = 0; i < m_dropsList.Length; i++)
            {
                m_dropsList[i].SetLowerChance(currentChance);
                currentChance += m_dropsList[i].Weight/totalWeight * 100f;
                m_dropsList[i].SetUpperChance(currentChance);
            }
        }

        public GameObject GetNextLoot(float chance)
        {
            for (int i = 0; i < m_dropsList.Length; i++)
            {
                if (m_dropsList[i].LowerChance <= chance && m_dropsList[i].UpperChance >= chance)
                {
                    return m_dropsList[i].LootPrefab;
                }
            }
            return null;
        }
        
        public int CoinDropAmount => Random.Range(m_minCoinDrop, m_maxCoinDrop + 1);
        public int KeyDropAmount
        {
            get
            {
                if (m_chanceToDropKey <= 0) return 0;
                var chance = Random.Range(0, 100);
                return chance <= m_chanceToDropKey ? Random.Range(m_minKeyDrop,m_maxKeyDrop) : 0;
            }
        }

        public int SmallExpDropAmount => Random.Range(m_minSmallExpDrop, m_maxSmallExpDrop + 1);
        public int BigExpDropAmount
        {
            get
            {
                if (m_chanceToDropBigExp <= 0) return 0;
                var chance = Random.Range(0, 100);
                return chance <= m_chanceToDropBigExp ? Random.Range(m_minBigExpDrop, m_maxBigExpDrop + 1) : 0;
            }
        }

        public GameObject CoinPrefab=> m_coinPrefab;
        public GameObject KeyPrefab => m_keyPrefab;
        public GameObject SmallExpPrefab=> m_smallExpPrefab;
        public GameObject BigExpPrefab=> m_bigExpPrefab;
        
    }

    [Serializable]
    public class LootData
    {
        public GameObject LootPrefab;
        public float Weight;
        [SerializeField] [ReadOnly] private float m_lowerChance;
        [SerializeField] [ReadOnly] private float m_upperChance;
        
        public float LowerChance => m_lowerChance;
        public float UpperChance => m_upperChance;

        public void SetLowerChance(float value)
        {
            m_lowerChance = value;
        }

        public void SetUpperChance(float value)
        {
            m_upperChance = value;
        }
    }
}
