using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Healths;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Damages
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] protected float m_minDamage;
        [SerializeField] protected float m_maxDamage;
        [SerializeField] protected float m_additionalDamage;
        [SerializeField] protected float m_multiplyDamage;
        [SerializeField] protected float m_critDamage;
        [Header("Damageable")]
        [SerializeField] protected float m_damageableInvulnerableTime;
        [SerializeField] protected LayerMask m_damageableLayerMask;

        public Action<GameObject> OnHitDamageable;

        protected virtual void Start()
        {
            m_multiplyDamage = 1;
        }

        public void SetDamageInfo((float addition,float multiplier,float critical) damageInfo)
        {
            m_additionalDamage = damageInfo.addition;
            m_multiplyDamage = damageInfo.multiplier;
            m_critDamage = damageInfo.critical;
        }

        
        protected virtual float ComputeDamage()
        {
            var rawDamage = Mathf.Round(Random.Range(m_minDamage, m_maxDamage));
            var finalDamage = ((rawDamage + m_additionalDamage) * m_multiplyDamage) * m_critDamage;
            return finalDamage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_damageableLayerMask))
            {
                CauseDamageToDamageable(other.gameObject);
            }
        }

        protected virtual void CauseDamageToDamageable(GameObject target)
        {
            var health = target.GetComponent<Health>();
            if (health != null)
            {
                OnHitDamageable?.Invoke(target);
                health.TakeDamage(ComputeDamage(),this.gameObject,m_damageableInvulnerableTime);
            }
        }
    }
}

