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
        [Header("Damageable")]
        [SerializeField] protected float m_damageableInvulnerableTime;
        [SerializeField] protected LayerMask m_damageableLayerMask;

        public Action<GameObject> OnHitDamageable;
        
        protected virtual float GetDamage()
        {
            return Random.Range(m_minDamage, m_maxDamage);
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

