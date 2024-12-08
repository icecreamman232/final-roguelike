using System.Collections.Generic;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class HealthBarUIController : MonoBehaviour
    {
        [SerializeField] private HeartUIController m_heartPrefab;
        [SerializeField] private PlayerHealthUpdateEvent m_playerHealthUpdateEvent;
        [SerializeField] private List<HeartUIController> m_heartList;
        
        private void Awake()
        {
            m_playerHealthUpdateEvent.AddListener(OnPlayerHealthUpdate);
            m_heartList = new List<HeartUIController>();
        }

        private void OnDestroy()
        {
            m_playerHealthUpdateEvent.RemoveListener(OnPlayerHealthUpdate);
        }

        private void Initialize(float maxHealthValue)
        {
            var heartCount = maxHealthValue / PlayerHealth.c_HealthPerHeart;
            for (int i = 0; i < heartCount; i++)
            {
                m_heartList.Add(Instantiate(m_heartPrefab, transform));
            }
        }

        private void OnPlayerHealthUpdate(float current, float max, GameObject source)
        {
            if (current == max)
            {
                Initialize(max);
            }
            else
            {
                UpdateHealthBar(current, max);
            }
        }

        private void UpdateHealthBar(float current, float max)
        {
            var heartCount = current / PlayerHealth.c_HealthPerHeart;
            for (int i = 0; i < m_heartList.Count; i++)
            {
                if (i < heartCount)
                {
                    m_heartList[i].SetFull();
                }
                else
                {
                    m_heartList[i].SetEmpty();
                }
            }
        }
    }
}

