using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Shoot Pattern")]
    public class ShootPatternData : ScriptableObject
    {
        public ProjectilePattern[] ShootPattern;
    }

    [Serializable]
    public class ProjectilePattern
    {
        public bool IsRelative;
        public float Speed;
        public float Angle;
    }
}

