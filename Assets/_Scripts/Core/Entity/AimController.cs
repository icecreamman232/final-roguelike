using System;
using System.Collections.Generic;
using UnityEngine;

namespace SGGames.Scripts.Core
{
    [Serializable]
    public struct AimTransform : IEquatable<AimTransform>
    {
        public Transform Transform;
        public float OffsetFromCenter;
        public float OffsetAngle;

        public bool Equals(AimTransform other)
        {
            return Equals(Transform, other.Transform);
        }

        public override bool Equals(object obj)
        {
            return obj is AimTransform other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Transform, OffsetFromCenter);
        }
    }
    
    /// <summary>
    /// This script will move and rotate registered transform following host transform direction.
    /// </summary>
    [Serializable]
    public class AimController
    {
        private Transform m_hostTransform;
        private List<AimTransform> m_positionTransformList;
        private List<AimTransform> m_rotationTransformList;


        public AimController(Transform hostTransform)
        {
            m_hostTransform = hostTransform;
            m_positionTransformList = new List<AimTransform>();
            m_rotationTransformList = new List<AimTransform>();
        }

        public void RegisterPositionTransform(AimTransform transform)
        {
            m_positionTransformList.Add(transform);
        }

        public void UnregisterPositionTransform(AimTransform transform)
        {
            m_positionTransformList.Remove(transform);
        }

        public void RegisterRotationTransform(AimTransform transform)
        {
            m_rotationTransformList.Add(transform);
        }

        public void UnregisterRotationTransform(AimTransform transform)
        {
            m_rotationTransformList.Remove(transform);
        }
        
        public void UpdatePosition(Vector2 inputDirection)
        {
            foreach(AimTransform aim in m_positionTransformList)
            {
                aim.Transform.position = m_hostTransform.position + (Vector3)(inputDirection * aim.OffsetFromCenter);
            }
        }

        public void UpdateRotation(Vector2 inputDirection)
        {
            foreach(AimTransform aim in m_rotationTransformList)
            {
                aim.Transform.rotation = Quaternion.AngleAxis(
                    Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg + aim.OffsetAngle,
                    Vector3.forward);
            }
        }
    }
}

