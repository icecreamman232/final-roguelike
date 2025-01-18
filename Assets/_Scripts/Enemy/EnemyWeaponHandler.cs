using SGGames.Scripts.Attribute;
using SGGames.Scripts.Core;
using SGGames.Scripts.Player;
using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyWeaponHandler : EnemyBehavior
    {
        [SerializeField] protected Weapon m_initialWeapon;
        [SerializeField][ReadOnly] protected Weapon m_currentWeapon;
        [SerializeField] protected Transform m_weaponAttachment;
        [SerializeField] protected float m_offsetFromCenter;
        [SerializeField] protected float m_offsetAngleWeapon;

        protected AimController m_aimController;
        protected EnemyController m_controller;
        private DamageInfo m_damageInfo;
        
        protected override void Start()
        {
            base.Start();
            m_aimController = new AimController(this.transform);
            m_aimController.RegisterRotationTransform(new AimTransform
            {
                Transform = m_weaponAttachment,
                OffsetFromCenter = m_offsetFromCenter,
                OffsetAngle = m_offsetAngleWeapon,
            });
            
            m_controller = GetComponent<EnemyController>();
            m_damageInfo = new DamageInfo()
            {
                AdditionMinDamage = 0,
                AdditionMaxDamage = 0,
                MultiplyDamage = 1,
                CriticalDamage = 1,
            };
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (m_currentWeapon == null)
            {
                EquipWeapon(m_initialWeapon);
            }
        }

        protected override void Update()
        {
            if (!m_isAllow) return;

            if (m_controller.CurrentBrain == null) return;
            if (m_controller.CurrentBrain.Target == null) return;
            
            var directionToTarget = (m_controller.CurrentBrain.Target.transform.position - m_weaponAttachment.position).normalized * m_offsetFromCenter;
            
            m_aimController.UpdateRotation(directionToTarget);
            m_weaponAttachment.position = transform.position + directionToTarget;
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
            m_currentWeapon.Shoot((target.position-transform.position).normalized, m_damageInfo);
        }

        public virtual void UseWeaponAtDirection(Vector2 direction)
        {
            if (!CanUseWeapon()) return;
            m_currentWeapon.Shoot(direction, m_damageInfo);
        }
    }
}

