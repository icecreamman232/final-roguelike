using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    [SelectionBase]
    public class PlayerHealth : Health
    {
        [SerializeField] private float m_armor;
        [SerializeField] private float m_dodgeRate;
        [SerializeField] private SpriteFlicker m_spriteFlicker;
        [SerializeField] private float m_flickerFrequency;
        [SerializeField] private PlayerHealthUpdateEvent m_PlayerHealthUpdateEvent;
        [SerializeField] private float m_regenerationRate;

        
        private readonly float m_regenerationInterval = 0.1f;
        private float m_regenerateTimer;
        
        /// <summary>
        /// Use this with caution since it could be null at some point.
        /// </summary>
        private GameObject m_lastSourceCauseDamage;
        
        public void Initialize(float maxHealth)
        {
            m_currentHealth = maxHealth;
            m_maxHealth = maxHealth;
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

            if (CanDodgeThisAttack())
            {
                return;
            }
            
            m_currentHealth -= damage * (1- m_armor/100);
            m_lastSourceCauseDamage = source;
            
            m_spriteFlicker.FlickerSprite(m_spriteRenderer,invincibilityDuration,m_flickerFrequency);
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

        private bool CanDodgeThisAttack()
        {
            var chance = Random.Range(0f, 100f);
            return chance <= m_dodgeRate;
        }

        public void AddRegenerationRate(float regenerationRate)
        {
            m_regenerationRate += regenerationRate;
        }

        public void AddArmor(float add)
        {
            m_armor += add;
        }
        
        public void SetArmor(float value)
        {
            m_armor = value;
        }

        public void AddDodgeRate(float dodgeRate)
        {
            m_dodgeRate += dodgeRate;
        }

        public void SetDodgeRate(float value)
        {
            m_dodgeRate = value;
        }
    }
}
