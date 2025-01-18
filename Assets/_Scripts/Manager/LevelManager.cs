using System;
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
using SGGames.Scripts.Tests;
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("Player")] 
        [SerializeField] private HeroData m_heroData;
        [SerializeField][ReadOnly] private GameObject m_playerRef;
        [Header("Area")]
        [SerializeField] private int m_currentAreaIndex;
        [Header("Room")]
        [SerializeField] private RoomGenerator m_roomGenerator;
        [SerializeField] private List<RoomData> m_leftSideRoomList;
        [SerializeField] private List<RoomData> m_rightSideRoomList;
        [SerializeField] private List<RoomRewardType> m_leftRoomRewardList;
        [SerializeField] private List<RoomRewardType> m_rightRoomRewardList;
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
        [SerializeField] private int m_enemyNumberInRoom;
        [Header("Events")] 
        [SerializeField] private ChangeRoomEvent m_changeRoomEvent;
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private IntEvent m_enterDoorEvent;
        [SerializeField] private BoolEvent m_freezePlayerEvent;
        [SerializeField] private BoolEvent m_freezeInputEvent;
        [SerializeField] private BoolEvent m_fadeOutScreenEvent;
        [Header("Debug")] 
        [SerializeField] private LevelManagerTestData m_testData;

        private readonly float m_delayBeforeEnemyAtk = 0.3f;
        private readonly int m_maxAreaCount = 7;
        private BoxCollider2D m_roomCollider;
        
        public Room CurrentRoom => m_currentRoom;
        public BoxCollider2D RoomCollider => m_roomCollider;
        
        public HeroData HeroData => m_heroData;
        
        public GameObject PlayerRef => m_playerRef;

        public Action<int> EnemyCountChangedAction;
        
        private void Start()
        {
            RandomController.SetSeed();
            m_roomIndex = 0;
            m_currentAreaIndex = 0;
            m_changeRoomEvent.Raise(m_currentAreaIndex,m_roomIndex);
            m_enterDoorEvent.AddListener(OnPlayerEnterDoor);

            if (m_testData.UseTestData)
            {
                m_roomIndex = m_testData.RoomIndex;
                m_currentAreaIndex = m_testData.AreaIndex;
                //Room Layout
                m_leftSideRoomList = m_testData.LeftSideRoomTestList;
                m_rightSideRoomList = m_testData.RightSideRoomTestList;
            
                //Room rewards
                m_leftRoomRewardList = m_testData.LeftRoomRewardTestList;
                m_rightRoomRewardList = m_testData.RightRoomRewardTestList;
            }
            else
            {
                //Room Layout
                m_leftSideRoomList = m_roomGenerator.GetRooms(m_currentAreaIndex);
                m_rightSideRoomList = m_roomGenerator.GetRooms(m_currentAreaIndex);
            
                //Room rewards
                m_leftRoomRewardList = m_roomGenerator.GetRoomRewards(m_currentAreaIndex);
                m_rightRoomRewardList = m_roomGenerator.GetRoomRewards(m_currentAreaIndex);
            }
            
            StartCoroutine(OnLevelLoaded());
        }

        private void OnDestroy()
        {
            m_enterDoorEvent.RemoveListener(OnPlayerEnterDoor);
        }
        
        #region Loading Room Methods
        private IEnumerator OnLevelLoaded()
        {
            LoadRoom(0);
        
            yield return new WaitForEndOfFrame();

            m_playerRef = Instantiate(m_heroData.HeroPrefab, m_currentRoom.PlayerSpawnSpot.position,Quaternion.identity);
            
            yield return new WaitForEndOfFrame();
            m_freezePlayerEvent.Raise(true);
            m_freezeInputEvent.Raise(true);
            LoadEnemy();
  
            CameraController.Instance.SetTarget(m_playerRef.transform);
            CameraController.Instance.SetRoomCollider(m_currentRoom.RoomCollider);
            CameraController.Instance.SetCameraPosition(m_playerRef.transform.position);
            CameraController.Instance.SetPermission(true);
            
            m_fadeOutScreenEvent?.Raise(false);

            yield return new WaitForSeconds(ScreenFader.FadeDuration);
            yield return new WaitForSeconds(m_delayBeforeEnemyAtk);
    
            m_gameEvent.Raise(GameEventType.ENTER_THE_ROOM);
            m_freezePlayerEvent.Raise(false);
            m_freezeInputEvent.Raise(false);
        }
        
        private IEnumerator OnLoadNextRoom()
        {
            //Freeze player
            m_freezePlayerEvent.Raise(true);
            //Freeze input
            m_freezeInputEvent.Raise(true);
            
            m_fadeOutScreenEvent.Raise(true);
            yield return new WaitForSeconds(ScreenFader.FadeDuration);
            
            CameraController.Instance.SetPermission(false);
            
            Destroy(m_currentRoom.gameObject);

            m_enemyNumberInRoom = 0;
            
            IncreaseRoomAndAreaIndex();
            
            m_changeRoomEvent.Raise(m_currentAreaIndex,m_roomIndex);

            var isBossRoom = m_roomIndex >= RoomGenerator.C_MAX_ROOM_NUMBER;
            
            if (isBossRoom)
            {
                LoadBossRoom(m_currentAreaIndex);
            }
            else
            {
                LoadRoom(m_roomIndex);
                LoadEnemy();
            }
            
            yield return new WaitForEndOfFrame();
  
            m_playerRef.transform.position = m_currentRoom.PlayerSpawnSpot.position;
            CameraController.Instance.SetRoomCollider(m_currentRoom.RoomCollider);
            CameraController.Instance.SetCameraPosition(m_currentRoom.PlayerSpawnSpot.position);
            CameraController.Instance.SetPermission(true);
            
            m_fadeOutScreenEvent?.Raise(false);
            yield return new WaitForSeconds(ScreenFader.FadeDuration);
            yield return new WaitForSeconds(m_delayBeforeEnemyAtk);
            m_gameEvent.Raise(GameEventType.ENTER_THE_ROOM);
            if (!isBossRoom)
            {
                m_freezePlayerEvent.Raise(false);
                m_freezeInputEvent.Raise(false);
            }
        }
        
        #endregion

        private void OnEnemyDeath(EnemyHealth enemyHealth)
        {
            enemyHealth.OnEnemyDeath -= OnEnemyDeath;
            m_enemyNumberInRoom--;
            EnemyCountChangedAction?.Invoke(m_enemyNumberInRoom);
            if (m_enemyNumberInRoom <= 0)
            {
                m_currentRoom.OpenDoors();
                m_gameEvent?.Raise(GameEventType.ROOM_CLEARED);
            }
        }
        
        private void LoadEnemy()
        {
            for (int i = 0; i < m_currentRoomData.EnemyList.Length; i++)
            {
                var enemyPrefab = m_currentRoomData.EnemyList[i].EnemyPrefab;
                var spawnPos = m_currentRoomData.EnemyList[i].SpawnPosition;
                Instantiate(enemyPrefab,spawnPos,Quaternion.identity,m_currentRoom.transform);
            }
        }

        private void IncreaseRoomAndAreaIndex()
        {
            m_roomIndex++;
            if (m_roomIndex >=  RoomGenerator.C_MAX_ROOM_NUMBER + 1) // +1 for counting boss room
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
            
            Debug.Log($"<color=yellow>Load Room {m_roomIndex}: {m_currentRoomData.name}</color>");
            var roomObj = Instantiate(m_currentRoomData.RoomPrefab);
            m_currentRoom = roomObj.GetComponent<Room>();
            m_roomCollider = m_currentRoom.RoomCollider;
            
            AddRewardToRoom(doorIndex, m_currentRoom.ChestSpawnSpot.position,m_currentRoom.transform);
        }

        private void LoadBossRoom(int areaIndex)
        {
            var room = Instantiate(m_roomGenerator.GetBossRoom(areaIndex));
            m_currentRoom = room;
            m_roomCollider = m_currentRoom.RoomCollider;
            
            Debug.Log($"<color=yellow>Load Boss Room {room.name}</color>");
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
                // case RoomRewardType.Item:
                //     Instantiate(m_itemChest, spawnPos, Quaternion.identity, roomTransform);
                //     break;
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

        #region Public methods
        
        public void SetCharacterForPlayer(HeroData data)
        {
            m_heroData = data;
        }

        
        public bool IsPositionInsideRoomBoundary(Vector2 point)
        {
            return m_roomCollider.OverlapPoint(point);
        }

        public void AddEnemyNumberInRoom(int number)
        {
            m_enemyNumberInRoom += number;
            EnemyCountChangedAction?.Invoke(m_enemyNumberInRoom);
        }

        public void RegisterEnemyDeathEvent(EnemyHealth health)
        {
            health.OnEnemyDeath += OnEnemyDeath;
        }
        
        #endregion
    }
}

