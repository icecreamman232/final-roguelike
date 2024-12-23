using System.Collections.Generic;
using SGGames.Scripts.Common;
using SGGames.Scripts.Data;
using SGGames.Scripts.Rooms;
using UnityEngine;

namespace SGGames.Scripts.Manager
{
    public enum RoomRewardType
    {
        None,
        Key,
        Coin,
        Item,
        Weapon,
        Helmet,
        Armor,
        Boots,
        Gloves,
        Accessories,
        Charm,
        COMMON_ROOM_NUMBER, //Counter enum
        
        //Event room
        Coin_Monster = 100,
        Blood_Room,
        Trader,
        EVENT_ROOM_NUMBER //Counter enum
    }
    
    public class RoomGenerator : MonoBehaviour
    {
        [SerializeField] private AreaData[] m_areaDataList;
        
        public static int C_MAX_ROOM_NUMBER = 6;
        public static int C_MAX_AREA = 2;
        private readonly int C_CHALLENGE_ROOM_CHANCE = 45;

        #region Get Room Layout
        public List<RoomData> GetRooms(int areaIndex)
        {
            var newRooms = new List<RoomData>();
            for (int i = 0; i < C_MAX_ROOM_NUMBER; i++)
            {
                newRooms.Add(GetCurrentRoom(i,areaIndex));
            }

            return newRooms;
        }
        
      
        private RoomData GetCurrentRoom(int roomIndex, int areaIndex)
        {
            switch (roomIndex)
            {
                case <= 2:
                    return m_areaDataList[areaIndex].GetEasyRoom();
                case 3:
                case 4:
                case 5:
                    return m_areaDataList[areaIndex].GetHardRoom();
                case >= 6:
                {
                    var challengeRoomChance = RandomController.GetRandomIntInRange(0,100);
                    return challengeRoomChance <= C_CHALLENGE_ROOM_CHANCE 
                        ? m_areaDataList[areaIndex].GetChallengeRoom() 
                        : m_areaDataList[areaIndex].GetHardRoom();
                }
            }
        }
        #endregion
        
        #region Get Room Rewards

        public List<RoomRewardType> GetRoomRewards(int areaIndex)
        {
            var newRoomRewards = new List<RoomRewardType>();
            for (int i = 0; i < C_MAX_ROOM_NUMBER; i++)
            {
                newRoomRewards.Add(GetRoomReward());
            }
            return newRoomRewards;
        }

        private RoomRewardType GetRoomReward()
        {
            return (RoomRewardType)RandomController.GetRandomIntInRange(1,C_MAX_ROOM_NUMBER);
        }
        
        #endregion

        public Room GetBossRoom(int areaIndex)
        {
            return m_areaDataList[areaIndex].GetBossRoom();
        }
    } 
}

