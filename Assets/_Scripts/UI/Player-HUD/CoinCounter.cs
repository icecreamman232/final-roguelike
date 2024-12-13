using System;
using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private IntEvent m_coinCounterUpdateEvent;
        [SerializeField] private TextMeshProUGUI m_coinPickedText;

        private void Awake()
        {
            m_coinCounterUpdateEvent.AddListener(OnCoinPicked);
        }

        private void OnDestroy()
        {
            m_coinCounterUpdateEvent.RemoveListener(OnCoinPicked);
        }

        private void OnCoinPicked(int amount)
        {
            m_coinPickedText.text = amount.ToString();
        }
    }
}
