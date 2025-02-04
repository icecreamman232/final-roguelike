using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Damages;
using SGGames.Scripts.Data;
using SGGames.Scripts.EditorExtensions;
using SGGames.Scripts.Player;
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
        [SerializeField] protected ProjectileSettings m_projectileSettings;
        [SerializeField] protected bool m_isAlive;
        [SerializeField] protected Transform m_model;
        [SerializeField] protected DamageHandler m_damageHandler;
        [SerializeField][ReadOnly] protected int m_piercingCounter;
        [Header("Events")]
        [SerializeField] protected UnityEvent m_onEnable;
        [SerializeField] protected UnityEvent m_onDestroy;
        
        protected Vector2 m_wakeupPosition;
        protected YieldInstruction DelayBeforeDestructionCoroutine;
        protected ProjectileRuntimeParameter m_projectileRuntimeParameter;
        protected List<BaseProjectileBehavior> m_projectileBehavior;
        
        protected virtual void Awake()
        {
            m_damageHandler.OnHitDamageable += OnHitDamageable;
            m_damageHandler.OnHitNonDamageable += OnHitNonDamageable;
            DelayBeforeDestructionCoroutine = new WaitForSeconds(m_projectileSettings.DelayBeforeDestruction);
            
            m_projectileRuntimeParameter = new ProjectileRuntimeParameter(m_projectileSettings, this, m_model);

            m_projectileBehavior = new List<BaseProjectileBehavior>();
            foreach (var behavior in m_projectileSettings.BehaviorType)
            {
                var newBehavior = ProjectileBehaviorFactory.CreateProjectileBehavior(behavior, m_projectileRuntimeParameter);
                m_projectileBehavior.Add(newBehavior);
            }
        }

        private void OnEnable()
        {
            m_onEnable?.Invoke();
            m_piercingCounter = m_projectileSettings.PiercingNumber;
        }

        public void SetTarget(Transform target)
        {
            m_projectileRuntimeParameter.Target = target;
        }

        public virtual void Spawn(Vector2 position, Quaternion rotation, Vector2 direction, DamageInfo damageInfo)
        {
            m_projectileRuntimeParameter.ResetParameter();
            m_wakeupPosition = position;
            transform.position = position;
            m_model.rotation = rotation * Quaternion.AngleAxis(m_projectileSettings.OffsetRotationAngle, Vector3.forward);
            m_projectileRuntimeParameter.Direction = direction;
            m_damageHandler.SetDamageInfo(damageInfo);
            m_isAlive = true;
        }
        
        protected virtual void Update()
        {
            if (!m_isAlive) return;
            UpdateMovement();
            CheckRange();
        }
        
        protected virtual void OnHitDamageable(GameObject obj)
        {
            m_piercingCounter--;
            if (m_piercingCounter >= 0) return;
            DestroyProjectile();
        }

        protected virtual void OnHitNonDamageable(GameObject obj)
        {
            m_piercingCounter--;
            if (m_piercingCounter >= 0) return;
            DestroyProjectile();
        }

        protected virtual void CheckRange()
        {
            var dist = Vector2.Distance(transform.position, m_wakeupPosition);
            if (dist >= m_projectileSettings.Range)
            {
                DestroyProjectile();
            }
        }

        protected virtual void UpdateMovement()
        {
            if (m_projectileBehavior != null)
            {
                foreach (var behavior in m_projectileBehavior)
                {
                    behavior.UpdateProjectile();
                }
            }
            else
            {
                transform.Translate(m_projectileRuntimeParameter.Direction * (m_projectileRuntimeParameter.CurrentSpeed * Time.deltaTime));
            }
        }

        public virtual void DestroyProjectile()
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
            m_damageHandler.OnHitNonDamageable -= OnHitNonDamageable;
        }
        
        #if UNITY_EDITOR
        public void ApplyData(ProjectileSettings settings, float minDamage, float maxDamage)
        {
            m_projectileSettings = settings;
            m_damageHandler.Initialize(minDamage, maxDamage);
        }
        
        #endif
    }
}

