using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Manager
{
    public class InGameProgressManager : Singleton<InGameProgressManager>
    {
        [SerializeField] private int m_currentExp;
        [SerializeField] private int m_maxExp;
        [SerializeField] private int m_currentLevel;
        [SerializeField] private PlayerExpData m_expData;
        [SerializeField] private IntEvent m_expPickedEvent;
        [SerializeField] private IntEvent m_playerLevelUpEvent;
        [SerializeField] private UpdateExpBarEvent m_updateExpBarEvent;

        public int CurrentLevel => m_currentLevel;
        
        private void Start()
        {
            m_currentLevel = 1;
            m_currentExp = 0;
            m_maxExp = m_expData.GetMaxExpForLevel(m_currentLevel);
            
            m_updateExpBarEvent?.Raise(m_currentExp,m_maxExp,m_currentLevel);
            m_expPickedEvent.AddListener(OnExpShardPicked);
        }
        
        private void OnDestroy()
        {
            m_expPickedEvent.RemoveListener(OnExpShardPicked);
        }
        
        private void OnExpShardPicked(int expValue)
        {
            m_currentExp += expValue;
            if (m_currentExp >= m_maxExp)
            {
                m_currentExp = 0;
                m_currentLevel++;
                m_maxExp = m_expData.GetMaxExpForLevel(m_currentLevel);
                m_playerLevelUpEvent?.Raise(m_currentLevel);
            }
            m_updateExpBarEvent?.Raise(m_currentExp,m_maxExp,m_currentLevel);
        }

        #if UNITY_EDITOR
        [ContextMenu("Lvl Up")]
        private void LevelUpTest()
        {
            m_currentExp = 0;
            m_currentLevel++;
            m_maxExp = m_expData.GetMaxExpForLevel(m_currentLevel);
            m_playerLevelUpEvent?.Raise(m_currentLevel);
        }
        #endif
    }
}
