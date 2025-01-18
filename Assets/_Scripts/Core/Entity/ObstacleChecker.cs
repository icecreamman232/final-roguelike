
using UnityEngine;

namespace SGGames.Scripts.Core
{
    public class ObstacleChecker
    {
        public bool IsColliderObstacle(Vector2 currentPosition, Vector2 size, Vector2 direction, float raycastDistance,LayerMask obstacleLayerMask)
        {
            var hit = Physics2D.BoxCast(currentPosition, size, 0, direction, raycastDistance, obstacleLayerMask);
            return hit.collider != null;
        }
    }
}

