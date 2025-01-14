using SGGames.Scripts.Weapons;
using UnityEngine;


namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "Projectile Settings", menuName = "SGGames/Data/Weapon/Projectile Settings")]
    public class ProjectileSettings : ScriptableObject
    {
        [Header("Common Settings")]
        [SerializeField] private ProjectileType m_type;
        [SerializeField] private float m_speed;
        [SerializeField] private float m_range;
        [SerializeField] private int m_piercingNumber;
        [SerializeField] private float m_delayBeforeDestruction;
        [Header("Visual Settings")]
        /// <summary>
        /// Offset angle of projectile visual.Ex: if projectile visual is up, the angle should be 90
        /// </summary>
        [SerializeField] private float m_offsetRotationAngle;
        
        public ProjectileType ProjectileType => m_type;
        public float Speed => m_speed;
        public float Range => m_range;
        public int PiercingNumber => m_piercingNumber;
        public float DelayBeforeDestruction => m_delayBeforeDestruction;
        public float OffsetRotationAngle => m_offsetRotationAngle;
    }
}

