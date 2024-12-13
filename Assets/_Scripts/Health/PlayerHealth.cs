using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class PlayerHealth : Health
    {
        [SerializeField] private PlayerHealthUpdateEvent m_PlayerHealthUpdateEvent;
        [SerializeField] private float m_regenerationRate;

        private readonly float m_regenerationInterval = 0.1f;
        private float m_regenerateTimer;
        
        /// <summary>
        /// Use this with caution since it could be null at some point.
        /// </summary>
        private GameObject m_lastSourceCauseDamage;

        protected override void Start()
        {
            base.Start();
            UpdateHealthBar();
        }

        private void Update()
        {
            if (m_isDead) return;
            m_regenerateTimer += Time.deltaTime;
            if (m_regenerateTimer >= m_regenerationInterval)
            {
                m_currentHealth += m_regenerationRate;
                m_currentHealth = Mathf.Clamp(m_currentHealth, 0, MaxHealth);
                UpdateHealthBar();

                m_regenerateTimer = 0;
            }
        }

        public override void TakeDamage(float damage, GameObject source, float invincibilityDuration, bool isCritical)
        {
            base.TakeDamage(damage, source, invincibilityDuration,isCritical);

            //Player cant take damage this frame
            if (!CanTakeDamage()) return;
            
            m_currentHealth -= damage;
            m_lastSourceCauseDamage = source;
            UpdateHealthBar();

            if (m_currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                StartCoroutine(OnInvulnerable(invincibilityDuration));
            }
        }

        protected override void UpdateHealthBar()
        {
            base.UpdateHealthBar();
            m_PlayerHealthUpdateEvent?.Raise(m_currentHealth,m_maxHealth,m_lastSourceCauseDamage);
        }

        protected override void Kill()
        {
            base.Kill();
            this.gameObject.SetActive(false);
        }

        public void AddRegenerationRate(float regenerationRate)
        {
            m_regenerationRate += regenerationRate;
        }
    }
}
