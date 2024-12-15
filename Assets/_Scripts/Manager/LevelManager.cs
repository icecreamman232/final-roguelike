using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Rooms;
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("Player")]
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField][ReadOnly] private GameObject m_playerRef;
        [Header("Area")]
        [SerializeField] private int m_currentAreaIndex;
        [Header("Room")]
        [SerializeField] private RoomGenerator m_roomGenerator;
        [SerializeField] private List<RoomData> m_leftSideRoomList;
        [SerializeField] private List<RoomData> m_rightSideRoomList;
        [SerializeField] private List<RoomRewardType> m_leftRoomRewardList;
        [SerializeField] private List<RoomRewardType> m_rightRoomRewardList;
        [SerializeField] private RoomData m_defaultRoom;
        [SerializeField][ReadOnly] private RoomData m_currentRoomData;
        [SerializeField][ReadOnly] private Room m_currentRoom;
        [SerializeField] private int m_roomIndex;
        [Header("Room Reward")]
        [SerializeField] private GameObject m_keyChest;
        [SerializeField] private GameObject m_coinChest;
        [SerializeField] private GameObject m_itemChest;
        [SerializeField] private GameObject m_weaponChest;
        [SerializeField] private GameObject m_helmetChest;
        [SerializeField] private GameObject m_armorChest;
        [SerializeField] private GameObject m_glovesChest;
        [SerializeField] private GameObject m_bootsChest;
        [SerializeField] private GameObject m_accessoriesChest;
        [SerializeField] private GameObject m_charmChest;
        
        [Header("Enemies")]
        [SerializeField]private List<EnemyHealth> m_enemyList;
        [Header("Events")] 
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private IntEvent m_enterDoorEvent;
        [SerializeField] private BoolEvent m_freezePlayerEvent;
        [SerializeField] private BoolEvent m_fadeOutScreenEvent;
        
        private readonly int m_maxAreaCount = 7;
        private BoxCollider2D m_roomCollider;
        
        public BoxCollider2D RoomCollider => m_roomCollider;
        
        private void Start()
        {
            m_roomIndex = 0;
            m_currentAreaIndex = 0;
            m_enterDoorEvent.AddListener(OnPlayerEnterDoor);

            //Room Layout
            m_leftSideRoomList = m_roomGenerator.GetRooms(m_currentAreaIndex);
            m_rightSideRoomList = m_roomGenerator.GetRooms(m_currentAreaIndex);
            
            //Room rewards
            m_leftRoomRewardList = m_roomGenerator.GetRoomRewards(m_currentAreaIndex);
            m_rightRoomRewardList = m_roomGenerator.GetRoomRewards(m_currentAreaIndex);
            
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
                m_gameEvent?.Raise(GameEventType.ROOM_CLEARED);
            }
        }
        
        
        private IEnumerator OnLevelLoaded()
        {
            m_currentRoomData = m_defaultRoom;
            Debug.Log($"<color=yellow>Load Room: {m_currentRoomData.name}</color>");
            var roomObj = Instantiate(m_currentRoomData.RoomPrefab);
            m_currentRoom = roomObj.GetComponent<Room>();
            m_roomCollider = m_currentRoom.RoomCollider;

            yield return new WaitForEndOfFrame();

            m_playerRef = Instantiate(m_playerPrefab, m_currentRoom.PlayerSpawnSpot.position,Quaternion.identity);
            
            yield return new WaitForEndOfFrame();
            m_freezePlayerEvent?.Raise(true);
            LoadEnemy();
            
            CameraController.Instance.SetTarget(m_playerRef.transform);
            CameraController.Instance.SetRoomCollider(m_currentRoom.RoomCollider);
            CameraController.Instance.SetCameraPosition(m_playerRef.transform.position);
            CameraController.Instance.SetPermission(true);
            
            m_fadeOutScreenEvent?.Raise(false);

            yield return new WaitForSeconds(ScreenFader.FadeDuration);
            
            m_gameEvent?.Raise(GameEventType.ENTER_THE_ROOM);
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
            if (m_roomIndex >=  RoomGenerator.C_MAX_ROOM_NUMBER)
            {
                m_roomIndex = 0;
                m_currentAreaIndex++;
                if (m_currentAreaIndex >= RoomGenerator.C_MAX_AREA)
                {
                    m_currentAreaIndex = RoomGenerator.C_MAX_AREA - 1;
                }
            }
        }

        private void LoadRoom(int doorIndex)
        {
            m_currentRoomData = doorIndex == 0 ? m_leftSideRoomList[m_roomIndex] : m_rightSideRoomList[m_roomIndex];
            Debug.Log($"<color=yellow>Load Room: {m_currentRoomData.name}</color>");
            var roomObj = Instantiate(m_currentRoomData.RoomPrefab);
            m_currentRoom = roomObj.GetComponent<Room>();
            m_roomCollider = m_currentRoom.RoomCollider;
            
            AddRewardToRoom(doorIndex, m_currentRoom.ChestSpawnSpot.position,m_currentRoom.transform);
        }

        private void AddRewardToRoom(int doorIndex,Vector2 spawnPos, Transform roomTransform)
        {
            var rewardType = doorIndex == 0 ? m_leftRoomRewardList[m_roomIndex] : m_rightRoomRewardList[m_roomIndex];
            Debug.Log($"<color=yellow>Room Reward: {rewardType}</color>");
            switch (rewardType)
            {
                case RoomRewardType.Key:
                    Instantiate(m_keyChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Coin:
                    Instantiate(m_coinChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Item:
                    Instantiate(m_itemChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Weapon:
                    Instantiate(m_weaponChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Helmet:
                    Instantiate(m_helmetChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Armor:
                    Instantiate(m_armorChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Boots:
                    Instantiate(m_bootsChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Gloves:
                    Instantiate(m_glovesChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Accessories:
                    Instantiate(m_accessoriesChest, spawnPos, Quaternion.identity, roomTransform);
                    break;
                case RoomRewardType.Charm:
                    Instantiate(m_charmChest, spawnPos, Quaternion.identity, roomTransform);
                    break;

         
                case RoomRewardType.Coin_Monster:
                    break;
                case RoomRewardType.Blood_Room:
                    break;
                case RoomRewardType.Trader:
                    break;
            }
        }

        private void OnPlayerEnterDoor(int doorIndex)
        {
            StartCoroutine(OnLoadNextRoom());
        }
        
        private IEnumerator OnLoadNextRoom()
        {
            //Freeze player
            m_freezePlayerEvent?.Raise(true);
            
            m_fadeOutScreenEvent?.Raise(true);
            yield return new WaitForSeconds(ScreenFader.FadeDuration);
            
            CameraController.Instance.SetPermission(false);
            
            Destroy(m_currentRoom.gameObject);

            IncreaseRoomAndAreaIndex();
            LoadRoom(m_roomIndex);
            LoadEnemy();
            
            yield return new WaitForEndOfFrame();
            
            m_playerRef.transform.position = m_currentRoom.PlayerSpawnSpot.position;
            CameraController.Instance.SetRoomCollider(m_currentRoom.RoomCollider);
            CameraController.Instance.SetCameraPosition(m_currentRoom.PlayerSpawnSpot.position);
            CameraController.Instance.SetPermission(true);
            
            m_fadeOutScreenEvent?.Raise(false);
            yield return new WaitForSeconds(ScreenFader.FadeDuration);
            
            m_gameEvent?.Raise(GameEventType.ENTER_THE_ROOM);
            m_freezePlayerEvent?.Raise(false);
        }

        public bool IsPositionInsideRoomBoundary(Vector2 point)
        {
            return m_roomCollider.OverlapPoint(point);
        }
    }
}

