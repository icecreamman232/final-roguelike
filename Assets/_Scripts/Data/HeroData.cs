using UnityEngine;

namespace SGGames.Scripts.Data
{
    public enum HeroID
    {
        Knight,
        Rogue,
        Mage,
    }
    
    [CreateAssetMenu(fileName = "HeroData", menuName = "SGGames/Data/Hero Data")]
    public class HeroData : ScriptableObject
    {
        [Header("Base Attributes")]
        [SerializeField] private HeroID m_heroID;
        [SerializeField] private int m_baseStrength;
        [SerializeField] private int m_baseAgility;
        [SerializeField] private int m_baseIntelligence;
        
        [Header("Offensive Attributes")]
        [SerializeField] private float m_criticalChance;
        [SerializeField] private float m_criticalDamage;
        
        public int BaseStrength => m_baseStrength;
        public int BaseAgility => m_baseAgility;
        public int BaseIntelligence => m_baseIntelligence;
        
        public float CriticalChance => m_criticalChance;
        public float CriticalDamage => m_criticalDamage;
    }
}

