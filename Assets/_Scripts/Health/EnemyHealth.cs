using System;
using System.Collections;
using MoreMountains.Feedbacks;
using SGGames.Scripts.Core;
using SGGames.Scripts.Enemies;
using SGGames.Scripts.Managers;
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public struct EnemyHitInfo
    {
        public float DamageTaken;
        public bool IsCritical;
        public GameObject Source;
    }
    
    public class EnemyHealth : Health
    {
        [SerializeField] protected EnemyController m_controller;
        [SerializeField] protected EnemyHealthBar m_healthBar;
        [SerializeField] protected float m_delayBeforeDeath;
        [SerializeField] protected MMF_Player m_deathFeedback;
        [SerializeField] protected GameObject m_weaponGroup;

        protected BoxCollider2D m_bodyCollider;
        
        protected EnemyMovement m_enemyMovement;
        public Action<EnemyHitInfo> OnHit;
        public Action<EnemyHealth> OnEnemyDeath;
        
        protected EnemyHitInfo m_hitInfo;

        protected override void Start()
        {
            base.Start();
            LevelManager.Instance.RegisterEnemyDeathEvent(this);
            ResetHealth();
            m_bodyCollider = GetComponent<BoxCollider2D>();
            m_enemyMovement = GetComponent<EnemyMovement>();
        }

        public override void TakeDamage(float damage, GameObject source, float invincibilityDuration, bool isCritical = false)
        {
            base.TakeDamage(damage, source, invincibilityDuration, isCritical);

            //Enemy cant take damage this frame
            if (!CanTakeDamage()) return;

            m_currentHealth -= damage;
            m_hitInfo.DamageTaken = damage;
            m_hitInfo.IsCritical = isCritical;
            m_hitInfo.Source = source;
            
            OnHit?.Invoke(m_hitInfo);

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
            m_healthBar.UpdateHealthBar(MathHelpers.Remap(m_currentHealth, 0, m_maxHealth, 0f, 1f));
        }

        protected override void Kill()
        {
            if(m_isDead) return;
            StartCoroutine(BeforeDeath());
        }

        protected virtual IEnumerator BeforeDeath()
        {
            base.Kill();
            OnEnemyDeath?.Invoke(this);
            m_bodyCollider.enabled = false;
            if (m_enemyMovement != null)
            {
                m_enemyMovement.StopMoving();
            }
            if (m_controller.CurrentBrain != null)
            {
                m_controller.CurrentBrain.ResetBrain();
                m_controller.CurrentBrain.BrainActive = false;
            }
            m_spriteRenderer.enabled = false;

            if (m_weaponGroup != null)
            {
                m_weaponGroup.SetActive(false);
            }
            
            m_deathFeedback.PlayFeedbacks();
            yield return new WaitForSeconds(m_delayBeforeDeath);
            this.gameObject.SetActive(false);
        }
        
        #if UNITY_EDITOR
        public void ApplyDataForHealth(float maxHealth)
        {
            m_maxHealth = maxHealth;
        }
        #endif
    }
}

