using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class KeyCounter : MonoBehaviour
    {
        [SerializeField] private IntEvent m_keyCounterUpdateEvent;
        [SerializeField] private TextMeshProUGUI m_keyPickedText;

        private void Awake()
        {
            m_keyCounterUpdateEvent.AddListener(OnKeyPicked);
        }

        private void OnDestroy()
        {
            m_keyCounterUpdateEvent.RemoveListener(OnKeyPicked);
        }

        private void OnKeyPicked(int amount)
        {
            m_keyPickedText.text = amount.ToString();
        }
    }
}

