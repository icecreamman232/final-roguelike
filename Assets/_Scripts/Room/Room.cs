using UnityEngine;

namespace SGGames.Scripts.Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D m_roomCollider;
        [SerializeField] private Transform m_playerSpawnSpot;
        [SerializeField] private Transform m_chestSpawnSpot;
        [SerializeField] private Door m_firstDoor;
        [SerializeField] private Door m_secondDoor;
        
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
    }
}
