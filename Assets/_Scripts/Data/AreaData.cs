using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Area Data")]
    public class AreaData : ScriptableObject
    {
        [SerializeField] private AreaName m_areaName;
        [SerializeField] private RoomData[] m_easyRooms;
        [SerializeField] private RoomData[] m_hardRooms;
        [SerializeField] private RoomData[] m_challengeRooms;

        public AreaName AreaName => m_areaName;
        
        public RoomData GetEasyRoom()
        {
            return m_easyRooms[Random.Range(0, m_easyRooms.Length)];
        }

        public RoomData GetHardRoom()
        {
            return m_hardRooms[Random.Range(0, m_easyRooms.Length)];
        }

        public RoomData GetChallengeRoom()
        {
            return m_challengeRooms[Random.Range(0, m_easyRooms.Length)];
        }
    }

    public enum AreaName
    {
        AREA_1,
        AREA_2,
        AREA_3,
        AREA_4,
        AREA_5,
        AREA_6,
        AREA_7,
    }
}
