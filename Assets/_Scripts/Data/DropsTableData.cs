using SGGames.Scripts.Core;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Managers;
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
        [Header("Healing Potion")]
        [SerializeField] private GameObject m_smallHealingPotionPrefab;
        [SerializeField] private GameObject m_mediumHealingPotionPrefab;
        [SerializeField] private GameObject m_grandeHealingPotionPrefab;
        
        [Header("Drops")]
        [SerializeField] private ItemContainer m_itemContainer;
        
        public GameObject GetNextLoot(Rarity rarity)
        {
            if (m_itemContainer == null) return null;
            switch (rarity)
            {
                case Rarity.Common:
                    return m_itemContainer.GetCommonItem();
                case Rarity.Uncommon:
                    return m_itemContainer.GetUncommonItem();
                case Rarity.Rare:
                    return m_itemContainer.GetRareItem();
                case Rarity.Epic:
                    return m_itemContainer.GetEpicItem();
                case Rarity.Legendary:
                    return m_itemContainer.GetLegendaryItem();
            }
            return null;
        }

        public GameObject GetNextPotion()
        {
            //Only drop healing potion if player's health is below 50%
            var playerHealth = ServiceLocator.GetService<PlayerHealth>();
            if (playerHealth.CurrentHealth > playerHealth.MaxHealth * 0.5f)
            {
                return null;
            }

            var currentPlayerLv = InGameProgressManager.Instance.CurrentLevel;
            
            if (currentPlayerLv <= 10)
            {
                return m_smallHealingPotionPrefab;
            }

            if (currentPlayerLv <= 20)
            {
                return m_mediumHealingPotionPrefab;
            }
            
            return m_grandeHealingPotionPrefab;
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
}
