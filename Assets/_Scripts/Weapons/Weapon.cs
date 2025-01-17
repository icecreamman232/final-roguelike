using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public enum WeaponState
    {
        READY,
        SHOOTING,
        SHOT,
        DELAY_BETWEEN_SHOTS,
    }
    
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponState m_currentState;
        [SerializeField] protected float m_baseDelayBetweenShots;
        [SerializeField] protected float m_currentDelayBetweenShots;
        [SerializeField] protected ObjectPooler m_projectilePooler;
        [SerializeField] protected float m_offsetFromCenter;
        [SerializeField] protected Transform m_model;
        [SerializeField] protected float m_offsetAngle;
        [SerializeField] protected Animator m_animator;
        [SerializeField] protected SpriteRenderer m_spriteRenderer;
        [SerializeField] protected Vector2 m_offsetSpawnPos;
        
        protected float m_delayTimer;
        protected PlayerAim m_playerAim;
        protected bool m_isOnLeft;
        
        public float BaseDelayBetweenShots => m_baseDelayBetweenShots;
       
        
        public WeaponState CurrentState => m_currentState;
        
        public void ApplyDelayBetweenShots(float delayBetweenShots)
        {
            m_currentDelayBetweenShots = delayBetweenShots;
        }

        public void Initialize(PlayerAim playerAim)
        {
            m_playerAim = playerAim;
        }
        
        public virtual void Shoot(Vector2 direction, DamageInfo damageInfo)
        {
            var projectileObj = m_projectilePooler.GetPooledGameObject();
            var projectile = projectileObj.GetComponent<Projectile>();
            var rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
            projectile.Spawn(transform.position, rotation,direction,damageInfo);
            m_currentState = WeaponState.SHOT;
        }

        public virtual void StopShooting()
        {
            m_currentState = WeaponState.READY;
            m_delayTimer = 0;
        }
        
        protected virtual void Update()
        {
            switch (m_currentState)
            {
                case WeaponState.READY:
                    break;
                case WeaponState.SHOT:
                    PlayUseWeaponAnimation();
                    m_delayTimer = m_currentDelayBetweenShots;
                    m_currentState = WeaponState.DELAY_BETWEEN_SHOTS;
                    break;
                case WeaponState.DELAY_BETWEEN_SHOTS:
                    m_delayTimer -= Time.deltaTime;
                    if (m_delayTimer <= 0)
                    {
                        m_currentState = WeaponState.READY;
                    }
                    break;
            }

            if (m_playerAim != null)
            {
                RotationWeaponModel();
            }
        }

        protected virtual void RotationWeaponModel()
        {
            m_model.position = transform.position + (Vector3)m_playerAim.AimDirection * m_offsetFromCenter;
            var angle = Mathf.Atan2(m_playerAim.AimDirection.y, m_playerAim.AimDirection.x) * Mathf.Rad2Deg;
            var flipAngle = 0f;
            m_isOnLeft = false;
            m_spriteRenderer.flipX = false;
            if (angle > 90 || angle < -90)
            {
                flipAngle = 180 + Mathf.Abs(m_offsetAngle) * 2;
                m_spriteRenderer.flipX = true;
                m_isOnLeft = true;
            }
            m_model.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(m_playerAim.AimDirection.y, m_playerAim.AimDirection.x) * Mathf.Rad2Deg + m_offsetAngle + flipAngle, Vector3.forward);
        }

        protected virtual void PlayUseWeaponAnimation()
        {
            
        }

        protected virtual void OnDestroy()
        {
            m_projectilePooler.CleanUp();    
        }
        
        #if UNITY_EDITOR
        public void ApplyData(WeaponData data)
        {
            m_baseDelayBetweenShots = data.BaseDelayBetweenShots;
        }

        public void ApplyData(float delayBetweenShots)
        {
            m_baseDelayBetweenShots = delayBetweenShots;
        }
        #endif
    }
}

