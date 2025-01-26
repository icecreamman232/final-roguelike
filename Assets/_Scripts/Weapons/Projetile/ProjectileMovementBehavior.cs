using System;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public abstract class ProjectileMovementBehavior : ScriptableObject
    {
        public abstract void Initialize();
        public abstract void UpdateMovement(ProjectileSettings settings, Transform entityTransform, Vector2 direction, float movementSpeed, Action<float> changeSpeedCallback = null);
    }
}


