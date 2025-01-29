namespace SGGames.Scripts.Weapons
{
    /// <summary>
    /// Factory class to create projectile behavior based on input type
    /// </summary>
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
                    return new HomingProjectileBehavior(parameter);
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
