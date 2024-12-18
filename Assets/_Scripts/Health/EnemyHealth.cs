using System;
using System.Collections;
using MoreMountains.Feedbacks;
using SGGames.Scripts.Core;
using SGGames.Scripts.Enemies;
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class EnemyHealth : Health
    {
        [SerializeField] protected EnemyController m_controller;
        [SerializeField] protected EnemyHealthBar m_healthBar;
        [SerializeField] protected float m_delayBeforeDeath;
        [SerializeField] protected MMF_Player m_deathFeedback;

        protected BoxCollider2D m_bodyCollider;
        
        protected EnemyMovement m_enemyMovement;
        public Action<float, bool> OnHit;
        public Action<EnemyHealth> OnEnemyDeath;

        protected override void Start()
        {
            base.Start();
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
            OnHit?.Invoke(damage,isCritical);

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
            m_enemyMovement.StopMoving();
            m_controller.CurrentBrain.ResetBrain();
            m_controller.CurrentBrain.BrainActive = false;
            m_spriteRenderer.enabled = false;
            m_deathFeedback.PlayFeedbacks();
            yield return new WaitForSeconds(m_delayBeforeDeath);
            this.gameObject.SetActive(false);
        }
    }
}

