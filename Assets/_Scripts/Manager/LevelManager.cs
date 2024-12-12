using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Data;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Rooms;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField][ReadOnly] private GameObject m_playerRef;
        [Header("Room")]
        [SerializeField] private RoomData m_currentRoomData;
        [SerializeField][ReadOnly] private Room m_currentRoom;
        [SerializeField]private List<EnemyHealth> m_enemyList;

        private void Start()
        {
            StartCoroutine(OnLevelLoaded());
        }

        public void SetPlayerPrefab(GameObject playerPrefab)
        {
            m_playerPrefab = playerPrefab;
        }

        private void OnEnemyDeath(EnemyHealth enemyHealth)
        {
            enemyHealth.OnEnemyDeath -= OnEnemyDeath;
            m_enemyList.Remove(enemyHealth);
            if (m_enemyList.Count <= 0)
            {
                m_currentRoom.OpenDoors();
            }
        }

        private IEnumerator OnLevelLoaded()
        {
            var roomObj = Instantiate(m_currentRoomData.RoomPrefab);
            m_currentRoom = roomObj.GetComponent<Room>();

            yield return new WaitForEndOfFrame();

            m_playerRef = Instantiate(m_playerPrefab, m_currentRoom.PlayerSpawnSpot);
            
            yield return new WaitForEndOfFrame();

            m_enemyList = new List<EnemyHealth>();
            for (int i = 0; i < m_currentRoomData.EnemyList.Length; i++)
            {
                var enemyPrefab = m_currentRoomData.EnemyList[i].EnemyPrefab;
                var spawnPos = m_currentRoomData.EnemyList[i].SpawnPosition;
                var enemyObj = Instantiate(enemyPrefab,spawnPos,Quaternion.identity);
                var enemyHealth = enemyObj.GetComponent<EnemyHealth>();
                enemyHealth.OnEnemyDeath += OnEnemyDeath;
                m_enemyList.Add(enemyHealth);
            }
            
            CameraController.Instance.SetTarget(m_playerRef.transform);
            CameraController.Instance.SetRoomCollider(m_currentRoom.RoomCollider);
            CameraController.Instance.SetPermission(true);
        }
        
        
    }
}

