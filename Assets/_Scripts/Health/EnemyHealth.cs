using SGGames.Scripts.Enemies;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class EnemyHealth : Health
    {
        [SerializeField] protected EnemyController m_controller;
        public override void TakeDamage(float damage, GameObject source, float invincibilityDuration)
        {
            base.TakeDamage(damage, source, invincibilityDuration);

            //Player cant take damage this frame
            if (!CanTakeDamage()) return;

            m_currentHealth -= damage;

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

        protected override void Kill()
        {
            base.Kill();
            m_controller.CurrentBrain.ResetBrain();
            m_controller.CurrentBrain.BrainActive = false;
            this.gameObject.SetActive(false);
        }
    }
}

