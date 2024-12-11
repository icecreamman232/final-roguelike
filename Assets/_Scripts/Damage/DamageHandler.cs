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
        [Header("Damageable")]
        [SerializeField] protected float m_damageableInvulnerableTime;
        [SerializeField] protected LayerMask m_damageableLayerMask;

        public Action<GameObject> OnHitDamageable;

        protected virtual void Start()
        {
            m_multiplyDamage = 1;
        }

        public void UpdateAdditionalDamage(float additionalDamage)
        {
            m_additionalDamage = additionalDamage;
        }

        public void UpdateMultiplyDamage(float multiplyDamage)
        {
            m_multiplyDamage = multiplyDamage;
        }
        
        protected virtual float GetDamage()
        {
            var rawDamage = Mathf.Round(Random.Range(m_minDamage, m_maxDamage));
            var finalDamage = (rawDamage + m_additionalDamage) * m_multiplyDamage;
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
                health.TakeDamage(GetDamage(),this.gameObject,m_damageableInvulnerableTime);
            }
        }
    }
}

