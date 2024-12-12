using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public enum WeaponState
    {
        READY,
        SHOT,
        DELAY_BETWEEN_SHOTS,
    }
    
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponState m_currentState;
        [SerializeField] protected float m_delayBetweenShots;
        [SerializeField] protected ObjectPooler m_projectilePooler;

        protected float m_delayTimer;
        
        public WeaponState CurrentState => m_currentState;
        
        public virtual void Shoot(Vector2 direction, (float additionDamage, float multiplierDamage, float criticalDamage) damageInfo = default)
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
                    m_delayTimer = m_delayBetweenShots;
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
        }
    }
}

