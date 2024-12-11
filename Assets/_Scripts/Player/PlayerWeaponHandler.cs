using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    /// <summary>
    /// Script that handle weapons for player such as: equip, switch, reload etc...
    /// </summary>
    public class PlayerWeaponHandler : PlayerBehavior
    {
        [SerializeField] protected Weapon m_initialWeapon;
        [SerializeField] protected Weapon m_currentWeapon;
        [SerializeField] protected Transform m_weaponAttachment;
        [SerializeField] protected PlayerAim m_playerAim;
        [SerializeField] protected PlayerDamageComputer m_playerDamageComputer;

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

        protected virtual void SwitchWeapon(Weapon newWeapon)
        {
            
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(0) && CanUseWeapon())
            {
                UseWeapon();
            }
        }

        protected virtual bool CanUseWeapon()
        {
            if (m_currentWeapon == null) return false;
            
            if (m_currentWeapon.CurrentState != WeaponState.READY) return false;
            
            return true;
        }

        protected virtual void UseWeapon()
        {
            m_currentWeapon.Shoot(m_playerAim.AimDirection, m_playerDamageComputer.AdditionDamage,m_playerDamageComputer.MultiplyDamage);
        }
    }
}

