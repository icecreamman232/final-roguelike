using SGGames.Scripts.Damages;
using UnityEngine;
using UnityEngine.Events;

namespace SGGames.Scripts.Weapons
{
    public enum ProjectileType
    {
        Melee,
        Ranged,
    }
    public class Projectile : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] protected bool m_isAlive;
        [SerializeField] protected ProjectileType m_ProjectileType;
        [SerializeField] protected float m_initialSpeed;
        [SerializeField] protected float m_currentSpeed;
        [SerializeField] protected float m_range;
        [SerializeField] protected Vector2 m_direction;
        [SerializeField] protected Transform m_model;
        [SerializeField] protected DamageHandler m_damageHandler;
        [SerializeField] protected UnityEvent m_onDestroy;
        
        /// <summary>
        /// Offset angle of projectile visual.Ex: if projectile visual is up, the angle should be 90
        /// </summary>
        [SerializeField] protected float m_offsetRotationAngle;
        
        protected Vector2 m_wakeupPosition;

        protected virtual void Start()
        {
            m_damageHandler.OnHitDamageable += OnHitDamageable;
        }

        public virtual void Spawn(Vector2 position, Quaternion rotation, Vector2 direction, float addDamage, float multiplyDamage)
        {
            ResetSpeed();
            m_wakeupPosition = position;
            transform.position = position;
            m_model.rotation = rotation * Quaternion.AngleAxis(m_offsetRotationAngle, Vector3.forward);
            m_direction = direction;
            m_damageHandler.UpdateAdditionalDamage(addDamage);
            m_damageHandler.UpdateMultiplyDamage(multiplyDamage);
            m_isAlive = true;
        }

        protected virtual void ResetSpeed()
        {
            m_currentSpeed = m_initialSpeed;
        }

        protected virtual void Update()
        {
            if (!m_isAlive) return;
            UpdateMovement();
            CheckRange();
        }
        
        protected virtual void OnHitDamageable(GameObject obj)
        {
            DestroyProjectile();
        }

        protected virtual void CheckRange()
        {
            var dist = Vector2.Distance(transform.position, m_wakeupPosition);
            if (dist >= m_range)
            {
                DestroyProjectile();
            }
        }

        protected virtual void UpdateMovement()
        {
            transform.Translate(m_direction * (m_currentSpeed * Time.deltaTime));
        }

        protected virtual void DestroyProjectile()
        {
            m_onDestroy?.Invoke();
            this.gameObject.SetActive(false);
            m_isAlive = false;
        }

        protected virtual void Destroy()
        {
            m_damageHandler.OnHitDamageable -= OnHitDamageable;
        }
    }
}

