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
        [Header("Prefab")] 
        [SerializeField] private GameObject m_heroPrefab;
        [Header("Base Attributes")]
        [SerializeField] private HeroID m_heroID;
        [SerializeField] private string m_heroName;
        [SerializeField] private float m_baseStrength;
        [SerializeField] private float m_baseAgility;
        [SerializeField] private float m_baseIntelligence;

        [Header("Offensive Attributes")]
        [SerializeField] private float m_criticalChance;
        [SerializeField] private float m_criticalDamage;
        
        [Header("Defense ability")]
        [SerializeField] private Sprite m_defenseAbilityEnableSprite;
        [SerializeField] private Sprite m_defenseAbilityDisableSprite;
        
        public GameObject HeroPrefab => m_heroPrefab;
        
        //Base
        public string HeroName => m_heroName;
        public float BaseStrength => m_baseStrength;
        public float BaseAgility => m_baseAgility;
        public float BaseIntelligence => m_baseIntelligence;

        public float CriticalChance => m_criticalChance;
        public float CriticalDamage => m_criticalDamage;
        
        public Sprite DefenseAbilityEnableSprite => m_defenseAbilityEnableSprite;
        public Sprite DefenseAbilityDisableSprite => m_defenseAbilityDisableSprite;
    }
}

