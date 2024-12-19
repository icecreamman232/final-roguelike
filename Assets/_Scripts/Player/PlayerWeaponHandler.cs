using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    /// <summary>
    /// Script that handle weapons for player such as: equip, switch, reload etc...
    /// </summary>
    public class PlayerWeaponHandler : PlayerBehavior
    {
        [SerializeField] private PlayerInventory m_playerInventory; 
        [SerializeField] protected WeaponData m_initialWeaponData;
        [SerializeField] protected Weapon m_currentWeapon;
        [SerializeField] protected Transform m_weaponAttachment;
        [SerializeField] protected PlayerAim m_playerAim;
        [SerializeField] protected PlayerDamageComputer m_playerDamageComputer;
        [SerializeField] protected PlayerAttributeController m_playerAttributeController;
        [Header("Events")]
        [SerializeField] private BoolEvent m_freezePlayerEvent;

        public bool IsWeaponInitialized => m_currentWeapon != null;
        public float BaseAtkTime => m_currentWeapon.BaseDelayBetweenShots;
        
        protected override void Start()
        {
            base.Start();
            m_freezePlayerEvent.AddListener(OnPlayerFreeze);
            Initialize();
        }

        private void OnDestroy()
        {
            m_freezePlayerEvent.RemoveListener(OnPlayerFreeze);
        }

        protected virtual void Initialize()
        {
            if (m_currentWeapon == null)
            {
                EquipWeapon(m_initialWeaponData.WeaponPrefab.GetComponent<Weapon>());
                m_playerInventory.AddInitialWeapon(m_initialWeaponData);
            }
        }
        
        public virtual void EquipWeapon(Weapon newWeapon)
        {
            m_currentWeapon = Instantiate(newWeapon, m_weaponAttachment);
            m_currentWeapon.Initialize(m_playerAim);
            ApplyAttackSpeedOnCurrentWeapon(m_playerAttributeController.ComputeDelayBetweenAttacks(BaseAtkTime));
        }

        public virtual void UnEquipWeapon()
        {
            m_currentWeapon.StopShooting();
            Destroy(m_currentWeapon.gameObject);
        }

        public virtual void SwitchWeapon(Weapon newWeapon)
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
            m_currentWeapon.Shoot(m_playerAim.AimDirection, m_playerDamageComputer.GetDamageInfo);
        }

        private void OnPlayerFreeze(bool isFreeze)
        {
            if (isFreeze)
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

