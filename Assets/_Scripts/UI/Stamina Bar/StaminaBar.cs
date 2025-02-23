using System.Collections.Generic;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private StaminaSlot m_staminaSlotPrefab;
        [SerializeField] private Transform m_staminaSlotContainer;
        [SerializeField] private StaminaUpdateEvent m_staminaUpdateEvent;

        private List<StaminaSlot> m_staminaSlots;
        private bool m_isInitialized;
        
        private void Awake()
        {
            m_staminaUpdateEvent.AddListener(OnStaminaUpdate);
            m_staminaSlots = new List<StaminaSlot>();
        }

        private void OnDestroy()
        {
            m_staminaUpdateEvent.RemoveListener(OnStaminaUpdate);
        }

        private void OnStaminaUpdate(int current, int max)
        {
            if (m_isInitialized)
            {
                if (m_staminaSlots.Count != max)
                {
                    RecreateStaminaBar(max);
                }
                else
                {
                    UpdateStaminaBar(current);
                }
            }
            else
            {
                Initialize(max);
                m_isInitialized = true;
            }
        }

        private void Initialize(int staminaAmount)
        {
            for (int i = 0; i < staminaAmount; i++)
            {
                var slot = Instantiate(m_staminaSlotPrefab, m_staminaSlotContainer);
                slot.SetFull();
                m_staminaSlots.Add(slot);
            }
        }

        private void RecreateStaminaBar(int max)
        {
            for (int i = 0; i < m_staminaSlots.Count; i++)
            {
                Destroy(m_staminaSlots[i].gameObject);
            }
            
            m_staminaSlots.Clear();

            Initialize(max);
        }

        private void UpdateStaminaBar(int current)
        {
            for (int i = 0; i < m_staminaSlots.Count; i++)
            {
                if (i < current)
                {
                    m_staminaSlots[i].SetFull();
                }
                else
                {
                    m_staminaSlots[i].SetEmpty();
                }
            }
        }
    }
}
