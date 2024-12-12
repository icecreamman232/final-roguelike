using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Manager
{
    public class InGameProgressManager : MonoBehaviour
    {
        [SerializeField] private int m_currentExp;
        [SerializeField] private IntEvent m_expPickedEvent;

        private void Start()
        {
            m_expPickedEvent.AddListener(OnExpShardPicked);
        }
        
        private void OnDestroy()
        {
            m_expPickedEvent.RemoveListener(OnExpShardPicked);
        }
        
        private void OnExpShardPicked(int expValue)
        {
            m_currentExp += expValue;
        }
    }
}
