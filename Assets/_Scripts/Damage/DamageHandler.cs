using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Enemies;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Damages
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] protected float m_minDamage;
        [SerializeField] protected float m_maxDamage;
        [SerializeField] protected float m_additionMin;
        [SerializeField] protected float m_additionMax;
        [SerializeField] protected float m_multiplyDamage;
        [SerializeField] protected float m_critDamage;
        [Header("Damageable")]
        [SerializeField] protected float m_damageableInvulnerableTime;
        [SerializeField] protected float m_knockBackForce;
        [SerializeField] protected float m_knockBackDuration;
        [SerializeField] protected float m_stunDuration;
        [SerializeField] protected LayerMask m_damageableLayerMask;
        [Header("NonDamageable")]
        [SerializeField] protected LayerMask m_nonDamageableLayerMask;

        public Action<GameObject> OnHitDamageable;
        public Action<GameObject> OnHitNonDamageable;

        protected virtual void Start()
        {
            m_multiplyDamage = 1;
            m_critDamage = 1;
        }

        public void Initialize(float minDmg, float maxDmg)
        {
            m_minDamage = minDmg;
            m_maxDamage = maxDmg;
        }

        public void SetDamageInfo(DamageInfo damageInfo)
        {
            m_additionMin = damageInfo.AdditionMinDamage;
            m_additionMax = damageInfo.AdditionMaxDamage;
            m_multiplyDamage = damageInfo.MultiplyDamage;
            m_critDamage = damageInfo.CriticalDamage;
            m_stunDuration = damageInfo.StunDuration;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_damageableLayerMask))
            {
                CauseDamageToDamageable(other.gameObject);
            }
            else if (LayerManager.IsInLayerMask(other.gameObject.layer, m_nonDamageableLayerMask))
            {
                CauseDamageToNonDamageable(other.gameObject);
            }
        }

        private void CauseDamageToNonDamageable(GameObject target)
        {
            OnHitNonDamageable?.Invoke(target);
        }

        protected virtual void CauseDamageToDamageable(GameObject target)
        {
            var health = target.GetComponent<Health>();
            if (health != null)
            {
                OnHitDamageable?.Invoke(target);
                health.TakeDamage(
                    PlayerDamageComputer.ComputeOutputDamage(m_minDamage,m_additionMin, m_maxDamage,m_additionMax,
                        m_multiplyDamage,m_critDamage,out var isCritical)
                    ,this.gameObject,m_damageableInvulnerableTime,isCritical);
            }

            //Only player applies knock back on enemies
            var enemyMovement = target.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                var attackDir = (target.transform.position - transform.position).normalized;
                enemyMovement.ApplyKnockBack(attackDir,m_knockBackForce,m_knockBackDuration);
                enemyMovement.ApplyStun(m_stunDuration);
            }

        }
    }
}

