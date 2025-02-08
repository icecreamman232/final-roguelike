using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Progression/Player Exp")]
    public class PlayerExpData : ScriptableObject
    {
        [SerializeField] private LevelData[] m_level;

        public int GetMaxExpForLevel(int level)
        {
            return m_level[level - 1].MaxXp;
        }
    }

    [Serializable]
    public struct LevelData
    {
        public int Level;
        public int MaxXp;
    }
}

