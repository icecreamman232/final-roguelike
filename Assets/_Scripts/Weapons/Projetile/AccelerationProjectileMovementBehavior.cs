using System;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    [CreateAssetMenu(menuName = "SGGames/Weapons/Movement Behaviors/Acceleration",order = 0)]
    public class AccelerationProjectileMovementBehavior : ProjectileMovementBehavior
    {
        [SerializeField] private float m_acceleration;
        [SerializeField] private float m_maxSpeed;

        public override void Initialize()
        {
            
        }
    
        public override void UpdateMovement(ProjectileSettings settings,Transform entityTransform, Vector2 direction, float movementSpeed, Action<float> changeSpeedCallback = null)
        {
            movementSpeed += m_acceleration * Time.deltaTime;
            if (movementSpeed > m_maxSpeed)
            {
                movementSpeed = m_maxSpeed;
            }
            
            entityTransform.Translate(direction * (movementSpeed * Time.deltaTime));
            changeSpeedCallback?.Invoke(movementSpeed);
        }
    }
}

