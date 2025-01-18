
using System.Collections.Generic;
using SGGames.Scripts.Data;
using SGGames.Scripts.Manager;
using UnityEngine;

namespace SGGames.Scripts.Tests
{
    [CreateAssetMenu(menuName = "SGGames/Test/Level Manager")]
    public class LevelManagerTestData : ScriptableObject
    {
        public bool UseTestData;
        public int RoomIndex;
        public int AreaIndex;
        public List<RoomData> LeftSideRoomTestList;
        public List<RoomData> RightSideRoomTestList;
        public List<RoomRewardType> LeftRoomRewardTestList;
        public List<RoomRewardType> RightRoomRewardTestList;
    }
}

