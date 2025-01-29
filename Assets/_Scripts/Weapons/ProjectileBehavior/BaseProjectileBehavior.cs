using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public enum ProjectileBehaviorType
    {
        Normal,
        Acceleration,
        Homing,
        Ricochet,
        Split,
        Piercing,
    }
    
    /// <summary>
    /// Base class for projectile behavior such as homing, ricochet, split etc...
    /// </summary>
    public abstract class BaseProjectileBehavior
    {
        protected ProjectileRuntimeParameter m_parameter;
        
        public BaseProjectileBehavior(ProjectileRuntimeParameter parameter)
        {
            m_parameter = parameter;
        }
        public abstract void UpdateProjectile();
    }
}

