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
        [SerializeField] private float m_baseStrength;
        [SerializeField] private float m_baseAgility;
        [SerializeField] private float m_baseIntelligence;
        [Header("Growth Attributes")]
        [SerializeField] private float m_growthStrength;
        [SerializeField] private float m_growthAgility;
        [SerializeField] private float m_growthIntelligence;
        
        [Header("Offensive Attributes")]
        [SerializeField] private float m_criticalChance;
        [SerializeField] private float m_criticalDamage;
        
        //Base
        public float BaseStrength => m_baseStrength;
        public float BaseAgility => m_baseAgility;
        public float BaseIntelligence => m_baseIntelligence;
        
        //Growth
        public float GrowthStrength => m_growthStrength;
        public float GrowthAgility => m_growthAgility;
        public float GrowthIntelligence => m_growthIntelligence;
        
        public float CriticalChance => m_criticalChance;
        public float CriticalDamage => m_criticalDamage;
    }
}

