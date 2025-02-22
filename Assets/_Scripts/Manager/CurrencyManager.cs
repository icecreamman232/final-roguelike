using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Manager
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [Header("Coin")]
        [SerializeField] private IntEvent m_coinPickedEvent;
        [SerializeField] private IntEvent m_updateCoinCounterEvent;
        [SerializeField] private int m_coinPicked;
        [Header("Key")]
        [SerializeField] private IntEvent m_keyPickedEvent;
        [SerializeField] private IntEvent m_updateKeyCounterEvent;
        [SerializeField] private int m_keyPicked;
        [Header("Modifier")] 
        [SerializeField] private float m_extraCoinEnemyMultiplier;
        [SerializeField] private float m_extraCoinChestMultiplier;
        [SerializeField] private int m_extraCoinForEnemy;
        [SerializeField] private int m_extraCoinForChest;
        
        public bool HasKey => m_keyPicked > 0;
        
        public int CurrentCoin => m_coinPicked;

        public float ExtraCoinEnemyMultiplier => m_extraCoinEnemyMultiplier;
        public float ExtraCoinChestMultiplier => m_extraCoinChestMultiplier;
        
        public int ExtraCoinForEnemy => m_extraCoinForEnemy;
        public int ExtraCoinForChest => m_extraCoinForChest;

        private void Start()
        {
            m_coinPickedEvent.AddListener(OnCoinPicked);
            m_updateCoinCounterEvent?.Raise(m_coinPicked);
            
            m_keyPickedEvent.AddListener(OnKeyPicked);
            m_updateKeyCounterEvent?.Raise(m_keyPicked);
        }

        private void OnDestroy()
        {
            m_coinPickedEvent.RemoveListener(OnCoinPicked);
            m_keyPickedEvent.RemoveListener(OnKeyPicked);
        }

        private void OnCoinPicked(int amount)
        {
            m_coinPicked += amount;
            m_updateCoinCounterEvent?.Raise(m_coinPicked);
        }
        
        private void OnKeyPicked(int amount)
        {
            m_keyPicked += amount;
            m_updateKeyCounterEvent?.Raise(m_keyPicked);
        }

        public void ConsumeKey(int amount)
        {
            m_keyPicked -= amount;
        }

        public void AddExtraCoinForEnemy(float amount)
        {
            m_extraCoinForEnemy += (int)amount;
        }

        public void AddExtraCoinForChest(float amount)
        {
            m_extraCoinForChest += (int)amount;
        }

        public void AddExtraCoinChestMultiplier(float add)
        {
            m_extraCoinChestMultiplier += add;
        }

        public void AddExtraCoinEnemyMultiplier(float add)
        {
            m_extraCoinEnemyMultiplier += add;
        }
    }
}

