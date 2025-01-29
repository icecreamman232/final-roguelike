
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
        public Projectile Projectile { get; set; }
        public Transform ModelProjectileTf { get; set; }
        public float CurrentSpeed { get; set; }
        public Vector2 Direction { get; set; }
        public float OffsetRotationAngle { get; set; }
        public Transform Target { get; set; }
        
       //Acceleration
       public float Acceleration { get; set; }
       public float MaxSpeed { get; set; }
       
       //Homing
       public float MaxHomingDuration { get; set; }

       public ProjectileRuntimeParameter(ProjectileSettings settings, Projectile projectile, Transform modelProjectileTf)
       {
           Projectile = projectile;
           ModelProjectileTf = modelProjectileTf;
           InitialSpeed = settings.Speed;
           CurrentSpeed = InitialSpeed;
           Acceleration = settings.Acceleration;
           MaxSpeed = settings.MaxSpeed;
           OffsetRotationAngle = settings.OffsetRotationAngle;
           MaxHomingDuration = settings.MaxHomingDuration;
       }

       public void ResetParameter()
       {
           CurrentSpeed = InitialSpeed;
       }
    }
}
