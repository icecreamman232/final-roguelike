using System;
using UnityEngine;

namespace SGGames.Scripts.Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Vector2 m_botLeftPivot;
        [SerializeField] private Vector2 m_topRightPivot;
        [SerializeField] private BoxCollider2D m_roomCollider;
        [SerializeField] private Transform m_playerSpawnSpot;
        [SerializeField] private Transform m_chestSpawnSpot;
        [SerializeField] private Door m_firstDoor;
        [SerializeField] private Door m_secondDoor;
        
        public (Vector2 BotLeft,Vector2 TopRight) SpawnPivots => (m_botLeftPivot, m_topRightPivot);
        
        public Transform PlayerSpawnSpot => m_playerSpawnSpot;
        public Transform ChestSpawnSpot => m_chestSpawnSpot;
        public BoxCollider2D RoomCollider => m_roomCollider;

        public void OpenDoors()
        {
            if (m_firstDoor != null)
            {
                m_firstDoor.Open();
            }
            
            if (m_secondDoor != null)
            {
                m_secondDoor.Open();
            }
        }

#if UNITY_EDITOR
        [SerializeField] private bool ShowDebug;

        private void OnDrawGizmos()
        {
            if (!ShowDebug) return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(m_botLeftPivot, new Vector2(m_topRightPivot.x, m_botLeftPivot.y));
            Gizmos.DrawLine(new Vector2(m_topRightPivot.x,m_botLeftPivot.y), m_topRightPivot);
            Gizmos.DrawLine(m_topRightPivot, new Vector2(m_botLeftPivot.x, m_topRightPivot.y));
            Gizmos.DrawLine(new Vector2(m_botLeftPivot.x,m_topRightPivot.y), m_botLeftPivot);
        }
#endif
    }
}
