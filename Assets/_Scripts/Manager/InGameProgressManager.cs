using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Manager
{
    public class InGameProgressManager : MonoBehaviour
    {
        [SerializeField] private int m_currentExp;
        [SerializeField] private int m_maxExp;
        [SerializeField] private int m_currentLevel;
        [SerializeField] private IntEvent m_expPickedEvent;
        [SerializeField] private UpdateExpBarEvent m_updateExpBarEvent;

        private void Start()
        {
            m_currentLevel = 1;
            m_currentExp = 0;
            m_maxExp = 5;
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
            m_updateExpBarEvent?.Raise(m_currentExp,m_maxExp,m_currentLevel);
        }
    }
}
