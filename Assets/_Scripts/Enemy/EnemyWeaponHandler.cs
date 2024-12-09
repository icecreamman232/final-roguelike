
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyWeaponHandler : EnemyBehavior
    {
        [SerializeField] protected Weapon m_initialWeapon;
        [SerializeField][ReadOnly] protected Weapon m_currentWeapon;
        [SerializeField] protected Transform m_weaponAttachment;
        
        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (m_currentWeapon == null)
            {
                EquipWeapon(m_initialWeapon);
            }
        }
        
        protected virtual void EquipWeapon(Weapon newWeapon)
        {
            m_currentWeapon = Instantiate(newWeapon, m_weaponAttachment);
        }
        
        protected virtual bool CanUseWeapon()
        {
            if (m_currentWeapon == null) return false;
            
            if (m_currentWeapon.CurrentState != WeaponState.READY) return false;
            
            return true;
        }

        public virtual void UseWeaponAtTarget(Transform target)
        {
            if (!CanUseWeapon()) return;
            m_currentWeapon.Shoot((target.position-transform.position).normalized);
        }

        public virtual void UseWeaponAtDirection(Vector2 direction)
        {
            if (!CanUseWeapon()) return;
            m_currentWeapon.Shoot(direction);
        }
    }
}

