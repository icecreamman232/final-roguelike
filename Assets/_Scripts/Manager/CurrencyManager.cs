using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Manager
{
    public class CurrencyManager : MonoBehaviour
    {
        [SerializeField] private IntEvent m_coinPickedEvent;
        [SerializeField] private int m_coinPicked;

        private void Start()
        {
            m_coinPickedEvent.AddListener(OnCoinPicked);
        }

        private void OnDestroy()
        {
            m_coinPickedEvent.RemoveListener(OnCoinPicked);
        }

        private void OnCoinPicked(int amount)
        {
            m_coinPicked += amount;
        }
    }
}

