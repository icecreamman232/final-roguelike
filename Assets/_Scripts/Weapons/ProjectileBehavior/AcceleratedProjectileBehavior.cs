using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class AcceleratedProjectileBehavior : BaseProjectileBehavior
    {
        public AcceleratedProjectileBehavior(ProjectileRuntimeParameter parameter) : base(parameter)
        {
        }

        public override void UpdateProjectile()
        {
            m_parameter.CurrentSpeed += m_parameter.Acceleration * Time.deltaTime;

            if (m_parameter.CurrentSpeed >= m_parameter.MaxSpeed)
            {
                m_parameter.CurrentSpeed = m_parameter.MaxSpeed;
            }
            
            m_parameter.ProjectileTransform.Translate(m_parameter.Direction * (m_parameter.CurrentSpeed * Time.deltaTime));
        }
    }
}
