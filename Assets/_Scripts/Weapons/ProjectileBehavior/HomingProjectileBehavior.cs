
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class HomingProjectileBehavior : BaseProjectileBehavior
    {
        private float m_travelledTime;
        
        public HomingProjectileBehavior(ProjectileRuntimeParameter parameter) : base(parameter)
        {
        }

        public override void UpdateProjectile()
        {
            var directionTowardTarget = (m_parameter.Target.position - m_parameter.Projectile.transform.position).normalized;
            var deltaMovement = m_parameter.CurrentSpeed * Time.deltaTime;
            Vector2 movement = directionTowardTarget * deltaMovement;
            
            m_parameter.Projectile.transform.Translate(movement);
            

            var angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + m_parameter.OffsetRotationAngle;
            
            m_parameter.ModelProjectileTf.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            
            m_travelledTime += Time.deltaTime;

            if (m_travelledTime >= m_parameter.MaxHomingDuration)
            {
                m_travelledTime = 0;
                m_parameter.Projectile.DestroyProjectile();
            }
        }
    }
}
