using System;
using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerStamina : PlayerBehavior, IGameService
    {
        [SerializeField] private int m_currentStamina;
        [SerializeField] private int m_maxStamina;

        private readonly float m_staminaRegenDuration = 3f;
        private float m_regenTimer;
        
        private void Awake()
        {
            ServiceLocator.RegisterService<PlayerStamina>(this);
        }

        public void Initialize(int maxStamina)
        {
            m_maxStamina = maxStamina;
            m_currentStamina = m_maxStamina;
        }

        protected override void Update()
        {
            if (m_currentStamina >= m_maxStamina) return;
            
            m_regenTimer += Time.deltaTime;
            if (m_regenTimer >= m_staminaRegenDuration)
            {
                m_regenTimer = 0;
                m_currentStamina++;
                m_currentStamina = Mathf.Clamp(m_currentStamina,0,m_maxStamina);
            }
            
            base.Update();
        }
        
        public bool HasStamina => m_currentStamina > 0;
        
        public void ConsumeStamina(int amount)
        {
            if (m_currentStamina <= 0) return;
            m_currentStamina -= amount;
            m_currentStamina = Mathf.Clamp(m_currentStamina,0,m_maxStamina);
        }
    }
}
