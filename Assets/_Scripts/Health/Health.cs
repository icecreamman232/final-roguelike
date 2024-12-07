using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    /// <summary>
    /// Base health class for both player and non-player entities
    /// </summary>
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float m_maxHealth;
        [SerializeField] protected float m_currentHealth;
        /// <summary>
        /// This boolean will be reset after duration
        /// </summary>
        [SerializeField] protected bool m_isInvulnerable;
        
        /// <summary>
        /// This boolean will be saved until it got changed.
        /// </summary>
        [SerializeField] protected bool m_isImmortal;
        [SerializeField] protected bool m_isDead;
        
        public float CurrentHealth => m_currentHealth;
        public float MaxHealth => m_maxHealth;
        public bool IsDead => m_isDead;

        public bool CanTakeDamageThisFrame => CanTakeDamage();

        public void ResetHealth()
        {
            m_currentHealth = m_maxHealth;
        }

        public void AddHealth(float amount)
        {
            m_currentHealth += amount;
        }

        public void OverrideHealth(float amount)
        {
            m_currentHealth = amount;
        }

        public void SetImmortal(bool immortal)
        {
            m_isImmortal = immortal;
        }
        
        protected virtual void Start()
        {
            ResetHealth();
        }

        protected virtual bool CanTakeDamage()
        {
            if (m_isImmortal) return false;

            if (m_isDead) return false;
            
            if (m_isInvulnerable) return false;
            
            if(m_currentHealth <= 0) return false;
            
            return true;
        }

        public virtual void TakeDamage(float damage, GameObject source, float invincibilityDuration)
        {
            
        }

        protected virtual void UpdateHealthBar()
        {
            
        }

        protected virtual IEnumerator OnInvulnerable(float duration)
        {
            m_isInvulnerable = true;
            yield return new WaitForSeconds(duration);
            m_isInvulnerable = false;
        }

        /// <summary>
        /// Kill process after entity have zero health value
        /// </summary>
        protected virtual void Kill()
        {
            m_isDead = true;
        }
    }
}

