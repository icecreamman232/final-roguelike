using System;
using System.Collections;
using SGGames.Scripts.Damages;
using SGGames.Scripts.Data;
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
        [SerializeField] protected float m_delayBeforeDestruction;
        [SerializeField] protected UnityEvent m_onEnable;
        [SerializeField] protected UnityEvent m_onDestroy;
        
        /// <summary>
        /// Offset angle of projectile visual.Ex: if projectile visual is up, the angle should be 90
        /// </summary>
        [SerializeField] protected float m_offsetRotationAngle;
        
        protected Vector2 m_wakeupPosition;
        protected YieldInstruction DelayBeforeDestructionCoroutine;
        
        protected virtual void Start()
        {
            m_damageHandler.OnHitDamageable += OnHitDamageable;
            DelayBeforeDestructionCoroutine = new WaitForSeconds(m_delayBeforeDestruction);
        }

        private void OnEnable()
        {
            m_onEnable?.Invoke();
        }

        public virtual void ApplyData(WeaponData data)
        {
            m_initialSpeed = data.ProjectileSpeed;
            m_range = data.AttackRange;
            m_damageHandler.Initialize(data.MinDamage, data.MaxDamage);
        }

        public virtual void Spawn(Vector2 position, Quaternion rotation, Vector2 direction, 
            (float addDamage, float multiplyDamage,float criticalDamage) damageInfo)
        {
            ResetSpeed();
            m_wakeupPosition = position;
            transform.position = position;
            m_model.rotation = rotation * Quaternion.AngleAxis(m_offsetRotationAngle, Vector3.forward);
            m_direction = direction;
            m_damageHandler.SetDamageInfo(damageInfo);
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
            if (!m_isAlive) return;
            StartCoroutine(OnDestroyingProjectile());
        }

        protected virtual IEnumerator OnDestroyingProjectile()
        {
            m_onDestroy?.Invoke();
            m_isAlive = false;
            yield return DelayBeforeDestructionCoroutine;
            this.gameObject.SetActive(false);
        }

        protected virtual void Destroy()
        {
            m_damageHandler.OnHitDamageable -= OnHitDamageable;
        }
    }
}

