using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "SGGames/Data/Room Data")]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private GameObject m_roomPrefab;
        [SerializeField] private EnemySpawnInfo[] m_enemyList;
        
        public GameObject RoomPrefab => m_roomPrefab;
        public EnemySpawnInfo[] EnemyList => m_enemyList;
    }

    [Serializable]
    public struct EnemySpawnInfo
    {
        public GameObject EnemyPrefab;
        public Vector2 SpawnPosition;
    }
}

