using System;
using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Rooms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("Player")]
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField][ReadOnly] private GameObject m_playerRef;
        [Header("Area")]
        [SerializeField] private int m_currentAreaIndex;
        [SerializeField] private AreaData[] m_areaDataList;
        [Header("Room")]
        [SerializeField] private RoomData m_currentRoomData;
        [SerializeField][ReadOnly] private Room m_currentRoom;
        [SerializeField] private int m_roomIndex;
        [SerializeField]private List<EnemyHealth> m_enemyList;
        [Header("Events")] 
        [SerializeField] private IntEvent m_enterDoorEvent;
        [SerializeField] private BoolEvent m_freezePlayerEvent;

        private readonly int m_maxRoomNumber = 6;
        private readonly int m_maxAreaCount = 7;
        private readonly int m_challengeRoomChance = 33;
        private BoxCollider2D m_roomCollider;
        
        public BoxCollider2D RoomCollider => m_roomCollider;
        
        private void Start()
        {
            m_roomIndex = 0;
            m_currentAreaIndex = 0;
            m_enterDoorEvent.AddListener(OnPlayerEnterDoor);
            StartCoroutine(OnLevelLoaded());
        }

        private void OnDestroy()
        {
            m_enterDoorEvent.RemoveListener(OnPlayerEnterDoor);
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

        private RoomData GetCurrentRoom()
        {
            switch (m_roomIndex)
            {
                case <= 1:
                    return m_areaDataList[m_currentAreaIndex].GetEasyRoom();
                case 3:
                case 4:
                case 5:
                    return m_areaDataList[m_currentAreaIndex].GetHardRoom();
                case >= 6:
                {
                    var challengeRoomChance = Random.Range(0, 100);
                    if (challengeRoomChance <= m_challengeRoomChance)
                    {
                        return m_areaDataList[m_currentAreaIndex].GetChallengeRoom();
                    }
                    else
                    {
                        return m_areaDataList[m_currentAreaIndex].GetHardRoom();
                    }
                }
                default:
                {
                    return m_areaDataList[m_currentAreaIndex].GetEasyRoom();
                }
            }
        }
        
        private IEnumerator OnLevelLoaded()
        {
            LoadRoom();

            yield return new WaitForEndOfFrame();

            m_playerRef = Instantiate(m_playerPrefab, m_currentRoom.PlayerSpawnSpot.position,Quaternion.identity);
            
            yield return new WaitForEndOfFrame();
            m_freezePlayerEvent?.Raise(true);
            LoadEnemy();
            
            CameraController.Instance.SetTarget(m_playerRef.transform);
            CameraController.Instance.SetRoomCollider(m_currentRoom.RoomCollider);
            CameraController.Instance.SetPermission(true);
            m_freezePlayerEvent?.Raise(false);
        }

        private void LoadEnemy()
        {
            m_enemyList = new List<EnemyHealth>();
            for (int i = 0; i < m_currentRoomData.EnemyList.Length; i++)
            {
                var enemyPrefab = m_currentRoomData.EnemyList[i].EnemyPrefab;
                var spawnPos = m_currentRoomData.EnemyList[i].SpawnPosition;
                var enemyObj = Instantiate(enemyPrefab,spawnPos,Quaternion.identity,m_currentRoom.transform);
                var enemyHealth = enemyObj.GetComponent<EnemyHealth>();
                enemyHealth.OnEnemyDeath += OnEnemyDeath;
                m_enemyList.Add(enemyHealth);
            }
        }

        private void IncreaseRoomAndAreaIndex()
        {
            m_roomIndex++;
            if (m_roomIndex >= m_maxRoomNumber)
            {
                m_roomIndex = 0;
                m_currentAreaIndex++;
                if (m_currentAreaIndex >= m_areaDataList.Length)
                {
                    m_currentAreaIndex = m_areaDataList.Length - 1;
                }
            }
        }

        private void LoadRoom()
        {
            m_currentRoomData = GetCurrentRoom();
            Debug.Log($"<color=yellow>Load Room: {m_currentRoomData.name}</color>");
            var roomObj = Instantiate(m_currentRoomData.RoomPrefab);
            m_currentRoom = roomObj.GetComponent<Room>();
            m_roomCollider = m_currentRoom.RoomCollider;
        }

        private void OnPlayerEnterDoor(int doorIndex)
        {
            StartCoroutine(OnLoadNextRoom());
        }
        
        private IEnumerator OnLoadNextRoom()
        {
            //Freeze player
            m_freezePlayerEvent?.Raise(true);
            CameraController.Instance.SetPermission(false);
            
            //TODO:Fade out screen
            
            Destroy(m_currentRoom.gameObject);

            IncreaseRoomAndAreaIndex();
            LoadRoom();
            
            //TODO:Load new room
            
            LoadEnemy();
            
            m_playerRef.transform.position = m_currentRoom.PlayerSpawnSpot.position;
            
            
            m_freezePlayerEvent?.Raise(false);
            
            CameraController.Instance.SetRoomCollider(m_currentRoom.RoomCollider);
            CameraController.Instance.SetPermission(true);
            
            yield return null;
        }

        public bool IsPositionInsideRoomBoundary(Vector2 point)
        {
            return m_roomCollider.OverlapPoint(point);
        }
    }
}

