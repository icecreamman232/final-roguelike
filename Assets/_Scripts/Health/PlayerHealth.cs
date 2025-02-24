using System;
using System.Collections;
using MoreMountains.Tools;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Healths
{
    public struct OnHitInfo
    {
        public bool IsDodged;
        public bool IsImmortal;
        public float DamageTaken;
    }
    
    [SelectionBase]
    public class PlayerHealth : Health, IPlayerHealthService
    {
        [SerializeField] private bool m_isImmortalFromDash;
        [SerializeField] private float m_chanceToNotTakeDamage;
        [SerializeField] private float m_percentDamageTaken;
        [SerializeField] private float m_armor;
        [SerializeField] private float m_dodgeRate;
        [SerializeField] private float m_flickerFrequency;
        [SerializeField] private float m_regenerationRate;
        [SerializeField] private int m_reviveTime;
        [Header("Events")]
        [SerializeField] private PlayerHealthUpdateEvent m_PlayerHealthUpdateEvent;
        [SerializeField] private PlayerEvent m_PlayerEvent;
        [SerializeField] private BoolEvent m_playerGainImmortalEvent;
        [SerializeField] private UpdateReviveEvent m_updateReviveEvent;
        [Header("Animation")]
        [SerializeField] private MMAnimationParameter m_deadAnim;
        
        private SpriteFlicker m_spriteFlicker;
        private readonly float m_regenerationInterval = 0.1f;
        private readonly string m_reviveVFXEventName = "PlayReviveVFX";
        private readonly float m_invulnerableDurationAfterRevive = 0.5f;
        private float m_regenerateTimer;
        private OnHitInfo m_onHitInfo;

        private bool CanRevive => m_reviveTime > 0;
        
        public Action<OnHitInfo> OnHit; //Boolean pass is for dodge chance
        
        public Action<float> OnHealing;
        public float Armor => m_armor;
        public float DodgeRate => m_dodgeRate;
        public float HPRegenerationRate => m_regenerationRate;
        
        
        /// <summary>
        /// Use this with caution since it could be null at some point.
        /// </summary>
        private GameObject m_lastSourceCauseDamage;

        private void Awake()
        {
            ServiceLocator.RegisterService<PlayerHealth>(this);
        }

        public void Initialize(float initialHealth, int reviveTime)
        {
            m_onHitInfo = new OnHitInfo();
            m_percentDamageTaken = 1;
            m_reviveTime = reviveTime;
            m_initialHealth = initialHealth;
            m_currentHealth = initialHealth;
            m_maxHealth = initialHealth;
            UpdateHealthBar();
            m_updateReviveEvent.Raise(m_reviveTime);
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
                OnChangeCurrentHealth?.Invoke(m_currentHealth);
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
            
            if (m_isImmortal) return false;

            if (m_isImmortalFromDash) return false;

            if (m_isDead) return false;
            
            if (m_isInvulnerable) return false;
            
            if(m_currentHealth <= 0) return false;

            return true;
        }
        
        private bool CanDodgeThisAttack()
        {
            var chance = Random.Range(0f, 100f);
            return chance <= m_dodgeRate;
        }
        
        public override void TakeDamage(float damage, GameObject source, float invincibilityDuration, bool isCritical = false)
        {
            damage = m_percentDamageTaken * damage;
            
            base.TakeDamage(damage, source, invincibilityDuration,isCritical);

            if (m_isImmortal)
            {
                m_onHitInfo.IsDodged = false;
                m_onHitInfo.IsImmortal = true;
                m_onHitInfo.DamageTaken = 0;
                OnHit?.Invoke(m_onHitInfo);
            }
            
            //Player cant take damage this frame
            if (!CanTakeDamage()) return;

            if (CanDodgeThisAttack())
            {
                m_onHitInfo.IsDodged = true;
                m_onHitInfo.IsImmortal = false;
                m_onHitInfo.DamageTaken = 0;
                OnHit?.Invoke(m_onHitInfo);
                m_PlayerEvent.Raise(PlayerEventType.ON_DODGE_ATTACK);
                return;
            }
            var finalDamage = damage * (1- m_armor/100);;
            m_currentHealth -= finalDamage;
            m_lastSourceCauseDamage = source;
            
            m_onHitInfo.IsDodged = false;
            m_onHitInfo.IsImmortal = false;
            m_onHitInfo.DamageTaken = finalDamage;
            OnHit?.Invoke(m_onHitInfo);
            
            OnChangeCurrentHealth?.Invoke(m_currentHealth);
            
            m_PlayerEvent.Raise(PlayerEventType.ON_BEING_HIT);
            
            m_spriteFlicker.FlickerSprite(m_spriteRenderer,invincibilityDuration,m_flickerFrequency);
            UpdateHealthBar();

            if (m_currentHealth <= 0)
            {
                if (CanRevive)
                {
                    Revive();
                    return;
                }
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

        #region Kill
        
        protected override void Kill()
        {
            if(m_isDead) return;
            StartCoroutine(OnKillRoutine());
        }

        private IEnumerator OnKillRoutine()
        {
            ServiceLocator.UnregisterService<IPlayerHealthService>();
            base.Kill();
            m_deadAnim.SetTrigger();
            yield return new WaitForSeconds(m_deadAnim.Duration);
            this.gameObject.SetActive(false);
        }
        
        #endregion

        #region Revive
        private void Revive()
        {
            Debug.Log("Player has been revived");
            StartCoroutine(OnReviveRoutine());
        }

        private IEnumerator OnReviveRoutine()
        {
            m_reviveTime--;
            m_updateReviveEvent.Raise(m_reviveTime);
            m_maxHealth = m_initialHealth;
            m_currentHealth = m_maxHealth;
            m_lastSourceCauseDamage = null;
            UpdateHealthBar();
            MMGameEvent.Trigger(m_reviveVFXEventName);
            
            //Set invulnerable after reviving
            m_isInvulnerable = true;
            yield return new WaitForSeconds(m_invulnerableDurationAfterRevive);
            m_isInvulnerable = false;

        }
        
        #endregion
        
        #region For modifier methods

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
            m_currentHealth += amount;
            if (m_currentHealth > m_maxHealth)
            {
                m_currentHealth = m_maxHealth;
            }
            OnChangeCurrentHealth?.Invoke(m_currentHealth);
            OnHealing?.Invoke(amount);
        }

        public void HealingPercentMaxHealth(float percent)
        {
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
        
        #endregion
        
        #region For Dash
        public void SetImmortalFromDash(bool isImmortal)
        {
            m_isImmortalFromDash = isImmortal;
        }
        #endregion
    }
}
