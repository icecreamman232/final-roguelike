using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Weapon/Shoot Pattern")]
    public class ShootPatternData : ScriptableObject
    {
        public ProjectilePattern[] ShootPattern;
    }

    [Serializable]
    public class ProjectilePattern
    {
        public bool IsRelative;
        public float Speed;
        [Header("Angles")]
        public float MinAngle;
        public float MaxAngle;
        [Header("Delay")]
        public float MinDelay;
        public float MaxDelay;

        public float GetAngle()
        {
            return Random.Range(MinAngle, MaxAngle);
        }

        public float GetDelay()
        {
            return Random.Range(MinDelay, MaxDelay);
        }
    }
}

