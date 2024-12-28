
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerMana : MonoBehaviour
    {
        [SerializeField][Min(0)] private float m_maxMana;
        [SerializeField][Min(0)] private float m_currentMana;
        [SerializeField][Min(0)] private float m_manaRegenRate;
        [SerializeField] private PlayerManaUpdateEvent m_playerUpdateManaEvent;
        
        public float MaxMana => m_maxMana;
        public float CurrentMana => m_currentMana;

        private float m_regenerateTimer;
        private readonly float m_regenerationInternal = 0.1f;
        
        public void Initialize(float maxMana)
        {
            m_maxMana = maxMana;
            m_currentMana = maxMana;
            UpdateManaBar();
        }

        public void AddManaRegeneration(float rate)
        {
            m_manaRegenRate += rate;
            UpdateManaBar();
        }

        public void SpentMana(float amount)
        {
            if (!CanSpentManaThisTime()) return;
            
            m_currentMana -=amount;
            if (m_currentMana < 0)
            {
                m_currentMana = 0;
            }
            UpdateManaBar();
        }

        public void AddMaxMana(float amount)
        {
            var percentOfCurrent = m_currentMana / m_maxMana;
            m_maxMana += amount;
            m_currentMana = percentOfCurrent * m_maxMana;
            UpdateManaBar();
        }

        public void AddCurrentMana(float amount)
        {
            m_currentMana += amount;
            if (m_currentMana >= m_maxMana)
            {
                m_currentMana = m_maxMana;
            }
            UpdateManaBar();
        }

        private void Update()
        {
            if (m_currentMana >= m_maxMana) return;

            m_regenerateTimer += Time.deltaTime;
            if (m_regenerateTimer >= m_regenerationInternal)
            {
                m_currentMana += m_manaRegenRate;
                m_currentMana = Mathf.Clamp(m_currentMana, 0, m_maxMana);
                UpdateManaBar();
                
                m_regenerateTimer = 0;
            }
        }

        private bool CanSpentManaThisTime()
        {
            if (m_currentMana <= 0) return false;
            
            return true;
        }

        private void UpdateManaBar()
        {
            m_playerUpdateManaEvent.Raise(m_currentMana,m_maxMana);
        }
    }
}

