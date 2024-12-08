
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class PlayerHealth : Health
    {
        [SerializeField] private PlayerHealthUpdateEvent m_PlayerHealthUpdateEvent;

        private static float c_healthPerHeart = 50f;
        
        public static float c_HealthPerHeart => c_healthPerHeart;
        
        /// <summary>
        /// Use this with caution since it could be null at some point.
        /// </summary>
        private GameObject m_lastSourceCauseDamage;

        protected override void Start()
        {
            base.Start();
            UpdateHealthBar();
        }

        public override void TakeDamage(float damage, GameObject source, float invincibilityDuration)
        {
            base.TakeDamage(damage, source, invincibilityDuration);

            //Player cant take damage this frame
            if (!CanTakeDamage()) return;

            //No matter how big the damage enemy cause, its always make player lost 1 heart per hit
            damage = c_healthPerHeart;
            
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
    }
}
