using System;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    /// <summary>
    /// Script that handle weapons for player such as: equip, switch, reload etc...
    /// </summary>
    public class PlayerWeaponHandler : PlayerBehavior, IPlayerWeaponService
    {
        [SerializeField] private WeaponData m_initialWeaponData;
        [SerializeField] private Weapon m_currentWeapon;
        [SerializeField] private Transform m_weaponAttachment;
        [SerializeField] private PlayerEvent m_playerEvent;
        
        private PlayerAttributeController m_playerAttributeController;
        
        private PlayerInventory m_playerInventory; 
        private PlayerAim m_playerAim;
        private PlayerDamageComputer m_playerDamageComputer;
        private WeaponData m_curWeaponData;
        
        public bool IsWeaponInitialized => m_currentWeapon != null;
        public float BaseAtkTime => m_currentWeapon.BaseDelayBetweenShots;

        public Action<WeaponData> OnEquipWeapon;
        public WeaponData CurrentWeaponData => m_curWeaponData;
        public Weapon CurrentWeapon => m_currentWeapon;
        
        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        protected virtual void Initialize()
        {
            m_playerInventory = GetComponent<PlayerInventory>();
            if (m_playerInventory == null)
            {
                Debug.LogError("Player Inventory Component is null");
            }
            
            m_playerAim = GetComponent<PlayerAim>();
            if (m_playerAim == null)
            {
                Debug.LogError("Player Aim Component is null");
            }
            
            m_playerDamageComputer = GetComponent<PlayerDamageComputer>();
            if (m_playerDamageComputer == null)
            {
                Debug.LogError("Player Damage Computer Component is null");
            }
            
            m_playerAttributeController = GetComponent<PlayerAttributeController>();
            if (m_playerAttributeController == null)
            {
                Debug.LogError("Player Attribute Controller is null");
            }
            
            if (m_currentWeapon == null)
            {
                EquipWeapon(m_initialWeaponData);
                m_playerInventory.AddInitialWeapon(m_initialWeaponData);
            }
            
            ServiceLocator.RegisterService<PlayerWeaponHandler>(this);
        }
        
        public virtual void EquipWeapon(WeaponData newWeapon)
        {
            m_currentWeapon = Instantiate(newWeapon.WeaponPrefab.GetComponent<Weapon>(), m_weaponAttachment);
            m_currentWeapon.Initialize(m_playerAim);

            //Apply modifier that weapon has if possible
            if (m_currentWeapon.TryGetComponent<WeaponModifierHolder>(out var weaponModifierHolder))
            {
                weaponModifierHolder.ApplyModifiers();
            }
            
            ApplyAttackSpeedOnCurrentWeapon(m_playerAttributeController.ComputeDelayBetweenAttacks(BaseAtkTime));

            m_curWeaponData = newWeapon;
            OnEquipWeapon?.Invoke(newWeapon);
        }

        public virtual void UnEquipWeapon()
        {
            m_currentWeapon.StopShooting();
            Destroy(m_currentWeapon.gameObject);
        }

        public virtual void SwitchWeapon(WeaponData newWeapon)
        {
            m_currentWeapon.StopShooting();
            Destroy(m_currentWeapon.gameObject);
            EquipWeapon(newWeapon);
        }

        protected override void Update()
        {
            base.Update();

            if (!m_isAllow) return;
            
            if (Input.GetMouseButton(0) && CanUseWeapon())
            {
                m_playerEvent.Raise(PlayerEventType.ON_ATTACK);
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
            m_currentWeapon.Shoot(m_playerAim.AimDirection, m_playerDamageComputer.GetDamageInfo());
        }

        public override void OnPlayerFreeze(bool isFrozen)
        {
            if (isFrozen)
            {
                m_currentWeapon.StopShooting();
                ToggleAllow(false);
            }
            else
            {
                ToggleAllow(true);
            }
        }

        public void ApplyAttackSpeedOnCurrentWeapon(float atkSpeed)
        {
            m_currentWeapon.ApplyDelayBetweenShots(atkSpeed);
        }
    }
}

