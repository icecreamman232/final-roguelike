using System;
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

        public GameObject GetNextLoot(float chance)
        {
            return null;
        }
        
        public int CoinDropAmount => Random.Range(m_minCoinDrop, m_maxCoinDrop + 1);
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
        public GameObject SmallExpPrefab=> m_smallExpPrefab;
        public GameObject BigExpPrefab=> m_bigExpPrefab;
        
    }

    [Serializable]
    public class LootData
    {
        public GameObject LootPrefab;
    }
}
