using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    public enum RoomDifficultType
    {
        Easy,
        Hard,
        Challenge
    }
    
    [CreateAssetMenu(fileName = "RoomData", menuName = "SGGames/Data/Room Data")]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private GameObject m_roomPrefab;
        [SerializeField] private EnemySpawnInfo[] m_enemyList;
        
        public GameObject RoomPrefab
        {
            get => m_roomPrefab;

            set => m_roomPrefab = value;
        }

        public EnemySpawnInfo[] EnemyList
        {
            get => m_enemyList;
            set => m_enemyList = value;
        }
    }

    [Serializable]
    public struct EnemySpawnInfo
    {
        public GameObject EnemyPrefab;
        public Vector2 SpawnPosition;
    }
}

