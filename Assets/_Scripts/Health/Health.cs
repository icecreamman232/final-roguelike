using System;
using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    /// <summary>
    /// Base health class for both player and non-player entities
    /// </summary>
    public class Health : MonoBehaviour
    {
        [SerializeField][Min(0)] protected float m_initialHealth;
        [SerializeField][Min(0)] protected float m_maxHealth;
        [SerializeField][Min(0)] protected float m_currentHealth;
        /// <summary>
        /// This boolean will be reset after duration
        /// </summary>
        [SerializeField] protected bool m_isInvulnerable;
        
        /// <summary>
        /// This boolean will be saved until it got changed.
        /// </summary>
        [SerializeField] protected bool m_isImmortal;
        [SerializeField] protected bool m_isDead;
        [SerializeField] protected SpriteRenderer m_spriteRenderer;
        
        public float CurrentHealth => m_currentHealth;
        public float MaxHealth => m_maxHealth;
        public bool IsDead => m_isDead;

        public bool CanTakeDamageThisFrame => CanTakeDamage();
        
        public Action<float> OnChangeCurrentHealth;
        public Action<float> OnChangeMaxHealth;
        
        public void ResetHealth()
        {
            m_maxHealth = m_initialHealth;
            m_currentHealth = m_initialHealth;
        }

        public virtual void ModifyCurrentHealth(float amount)
        {
            m_currentHealth += amount;
            m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_maxHealth);
            OnChangeCurrentHealth?.Invoke(m_currentHealth);
            UpdateHealthBar();
        }
        
        public void ModifyMaxHealth(float amount,bool isPercent)
        {
            var currentPercent = m_currentHealth / m_maxHealth;
            if (isPercent)
            {
                m_maxHealth += amount * m_maxHealth/100f;
            }
            else
            {
                m_maxHealth += amount;
            }
            
            m_currentHealth = m_maxHealth * currentPercent;
            OnChangeMaxHealth?.Invoke(m_maxHealth);
            OnChangeCurrentHealth?.Invoke(m_currentHealth);
            UpdateHealthBar();
        }

        public void OverrideMaxHealth(float amount)
        {
            var currentPercent = m_currentHealth / m_maxHealth;
            m_maxHealth = amount;
            m_currentHealth = m_maxHealth * currentPercent;
            OnChangeMaxHealth?.Invoke(m_maxHealth);
            UpdateHealthBar();
        }

        public virtual void SetImmortal(bool immortal)
        {
            m_isImmortal = immortal;
        }
        
        protected virtual void Start()
        {
            
        }

        protected virtual bool CanTakeDamage()
        {
            if (m_isImmortal) {return false;}

            if (m_isDead) return false;
            
            if (m_isInvulnerable) return false;
            
            if(m_currentHealth <= 0) return false;

            return true;
        }

        public virtual void TakeDamage(float damage, GameObject source, float invincibilityDuration, bool isCritical = false)
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

