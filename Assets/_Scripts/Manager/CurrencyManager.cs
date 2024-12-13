using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Manager
{
    public class CurrencyManager : MonoBehaviour
    {
        [Header("Coin")]
        [SerializeField] private IntEvent m_coinPickedEvent;
        [SerializeField] private IntEvent m_updateCoinCounterEvent;
        [SerializeField] private int m_coinPicked;
        [Header("Key")]
        [SerializeField] private IntEvent m_keyPickedEvent;
        [SerializeField] private IntEvent m_updateKeyCounterEvent;
        [SerializeField] private int m_keyPicked;

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
    }
}

