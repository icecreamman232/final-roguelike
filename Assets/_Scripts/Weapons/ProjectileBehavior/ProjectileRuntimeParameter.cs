
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    /// <summary>
    /// Main class to hold all runtime parameters that can be passed into projectile behavior class to modify projectile behavior.
    /// </summary>
    public class ProjectileRuntimeParameter
    {
        //Common parameters
        public float InitialSpeed { get; set; }
        public Transform ProjectileTransform { get; set; }
        public Transform ModelProjectileTf { get; set; }
        public float CurrentSpeed { get; set; }
        public Vector2 Direction { get; set; }
        
       //Acceleration
       public float Acceleration { get; set; }
       public float MaxSpeed { get; set; }

       public ProjectileRuntimeParameter(ProjectileSettings settings, Transform projectileTransform, Transform modelProjectileTf)
       {
           ProjectileTransform = projectileTransform;
           ModelProjectileTf = modelProjectileTf;
           InitialSpeed = settings.Speed;
           CurrentSpeed = InitialSpeed;
           Acceleration = settings.Acceleration;
           MaxSpeed = settings.MaxSpeed;
       }

       public void ResetParameter()
       {
           CurrentSpeed = InitialSpeed;
       }
    }
}
