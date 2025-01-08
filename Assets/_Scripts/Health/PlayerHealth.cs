using System;
using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Healths
{
    [SelectionBase]
    public class PlayerHealth : Health
    {
        [SerializeField] private float m_chanceToNotTakeDamage;
        [SerializeField] private float m_percentDamageTaken;
        [SerializeField] private float m_armor;
        [SerializeField] private float m_dodgeRate;
        [SerializeField] private float m_flickerFrequency;
        [SerializeField] private PlayerHealthUpdateEvent m_PlayerHealthUpdateEvent;
        [SerializeField] private float m_regenerationRate;
        [SerializeField] private PlayerEvent m_PlayerEvent;
        [SerializeField] private BoolEvent m_playerGainImmortalEvent;
        private SpriteFlicker m_spriteFlicker;
        private readonly float m_regenerationInterval = 0.1f;
        private float m_regenerateTimer;

        public Action<bool,bool> OnHit; //Boolean pass is for dodge chance
        public Action<float> OnHealing;
        public float Armor => m_armor;
        public float DodgeRate => m_dodgeRate;
        public float HPRegenerationRate => m_regenerationRate;
        
        /// <summary>
        /// Use this with caution since it could be null at some point.
        /// </summary>
        private GameObject m_lastSourceCauseDamage;
        
        public void Initialize(float maxHealth)
        {
            m_percentDamageTaken = 1;
            m_currentHealth = maxHealth;
            m_maxHealth = maxHealth;
            UpdateHealthBar();
            m_spriteFlicker = GetComponentInChildren<SpriteFlicker>();
            if (m_spriteFlicker == null)
            {
                Debug.LogError($"SpriteFlicker is null on {this.gameObject.name}");
            }
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

        protected override bool CanTakeDamage()
        {
            var chance = Random.Range(0f, 100f);
            if (chance <= m_chanceToNotTakeDamage)
            {
                return false;
            }
            
            return base.CanTakeDamage();

        }
        public override void TakeDamage(float damage, GameObject source, float invincibilityDuration, bool isCritical = false)
        {
            damage = m_percentDamageTaken * damage;
            
            base.TakeDamage(damage, source, invincibilityDuration,isCritical);

            if (m_isImmortal)
            {
                OnHit?.Invoke(false,true);
            }
            
            //Player cant take damage this frame
            if (!CanTakeDamage()) return;

            if (CanDodgeThisAttack())
            {
                OnHit?.Invoke(true,false);
                m_PlayerEvent.Raise(PlayerEventType.DODGE);
                return;
            }
            
            m_currentHealth -= damage * (1- m_armor/100);
            m_lastSourceCauseDamage = source;
            OnHit?.Invoke(false,false);
            
            m_PlayerEvent.Raise(PlayerEventType.TAKE_DAMAGE);
            
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

        public void ModifyChanceToNotTakeDamage(float chance)
        {
            m_chanceToNotTakeDamage += chance;
        }

        public void ModifyPercentDamageTaken(float addPercent)
        {
            m_percentDamageTaken += addPercent;
            if (m_percentDamageTaken < 0)
            {
                m_percentDamageTaken = 0;
            }
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

        public void HealingFlatAmount(float amount)
        {
            m_PlayerEvent.Raise(PlayerEventType.HEALING);
            m_currentHealth += amount;
            if (m_currentHealth > m_maxHealth)
            {
                m_currentHealth = m_maxHealth;
            }
            
            OnHealing?.Invoke(amount);
        }

        public void HealingPercentMaxHealth(float percent)
        {
            m_PlayerEvent.Raise(PlayerEventType.HEALING);
            var healingAmount = (percent / 100f) * m_maxHealth;
            m_currentHealth += healingAmount;
            if (m_currentHealth > m_maxHealth)
            {
                m_currentHealth = m_maxHealth;
            }
            
            OnHealing?.Invoke(healingAmount);
        }

        public override void SetImmortal(bool immortal)
        {
            base.SetImmortal(immortal);
            m_playerGainImmortalEvent.Raise(immortal);
        }
    }
}
