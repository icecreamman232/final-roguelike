using System;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    [CreateAssetMenu(menuName = "SGGames/Weapons/Movement Behaviors/Wave(Sin)",order = 1)]
    public class WaveProjectileMovementBehavior : ProjectileMovementBehavior
    {
        [SerializeField] private float m_frequency = 15; // Speed of the wave
        [SerializeField] private float m_amplitude = 0.01f;
        
        private Transform m_model;
        
        public override void Initialize()
        {
            
        }

        public override void UpdateMovement(ProjectileSettings settings,Transform entityTransform, Vector2 direction, float movementSpeed,
            Action<float> changeSpeedCallback = null)
        {
            // Calculate the sine wave offset
            float yOffset = Mathf.Sin( Time.time * m_frequency) * m_amplitude;

            // Move the object in the specified direction with sine wave
            Vector2 movement = direction * (movementSpeed * Time.deltaTime);
            movement.y += yOffset; // Add the sine wave effect

            entityTransform.position += (Vector3)movement; // Update the position

            if (m_model == null)
            {
                m_model = entityTransform.GetChild(0);
            }

            var angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + settings.OffsetRotationAngle;
            
            m_model.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        }
    }
}

