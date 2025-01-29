
using SGGames.Scripts.Data;

namespace SGGames.Scripts.Weapons
{
    public static class ProjectileBehaviorFactory
    {
        public static BaseProjectileBehavior CreateProjectileBehavior(ProjectileBehaviorType behaviorType, ProjectileRuntimeParameter parameter)
        {
            switch (behaviorType)
            {
                default:
                case ProjectileBehaviorType.Normal:
                    break;
                case ProjectileBehaviorType.Acceleration:
                    return new AcceleratedProjectileBehavior(parameter);
                case ProjectileBehaviorType.Homing:
                    break;
                case ProjectileBehaviorType.Ricochet:
                    break;
                case ProjectileBehaviorType.Split:
                    break;
                case ProjectileBehaviorType.Piercing:
                    break;
            }

            return null;
        }
    }

}
