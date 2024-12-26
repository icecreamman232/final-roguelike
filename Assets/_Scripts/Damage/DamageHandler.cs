using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

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
        }

        
        protected virtual float ComputeDamage(out bool isCritical)
        {
            var rawDamage = Mathf.Round(Random.Range(m_minDamage + m_additionMin, m_maxDamage + m_additionMax));
            var finalDamage = Mathf.Round(rawDamage * m_multiplyDamage * m_critDamage);
            isCritical = m_critDamage > 1;
            return finalDamage;
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
                health.TakeDamage(ComputeDamage(out var isCritical),this.gameObject,m_damageableInvulnerableTime,isCritical);
            }
        }
    }
}

