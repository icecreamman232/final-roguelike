using System;
using System.Collections.Generic;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class ReviveBar : MonoBehaviour
    {
        [SerializeField] private GameObject m_reviveSlotPrefab;
        [SerializeField] private Transform m_pivot;
        [SerializeField] private UpdateReviveEvent m_updateReviveEvent;
        
        private List<GameObject> m_reviveSlots;

        private void Awake()
        {
            m_updateReviveEvent.AddListener(OnUpdateReviveBar);
        }

        private void OnDestroy()
        {
            m_updateReviveEvent.RemoveListener(OnUpdateReviveBar);
        }

        private void Start()
        {
            m_reviveSlots = new List<GameObject>();
        }

        private void OnUpdateReviveBar(int current)
        {
            if (m_reviveSlots.Count > 0)
            {
                for (int i = 0; i < m_reviveSlots.Count; i++)
                {
                    Destroy(m_reviveSlots[i]);
                }
                
                m_reviveSlots.Clear();
            }

            for (int i = 0; i < current; i++)
            {
                m_reviveSlots.Add(Instantiate(m_reviveSlotPrefab,m_pivot));
            }
        }
    }
}

