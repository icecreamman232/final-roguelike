using SGGames.Scripts.Data;
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerAttributeController : PlayerBehavior
    {
        [Header("Base Attributes")]
        [SerializeField] private HeroData m_heroData;
        [SerializeField] private int m_strengthPoints;
        [SerializeField] private int m_agilityPoints;
        [SerializeField] private int m_intelligencePoints;
        
        [Header("Components")]
        [SerializeField] private PlayerHealth m_playerHealth;
        [SerializeField] private PlayerDamageComputer m_playerDamageComputer;
        
        private readonly float m_strengthToRegenerationRate = 0.05f;

        public int StrengthPoints => m_strengthPoints;
        public int AgilityPoints => m_agilityPoints;
        public int IntelligencePoints => m_intelligencePoints;
        
        protected override void Start()
        {
            base.Start();
            InitializeAttributes();
        }

        private void InitializeAttributes()
        {
            m_strengthPoints = m_heroData.BaseStrength;
            m_agilityPoints = m_heroData.BaseAgility;
            m_intelligencePoints = m_heroData.BaseIntelligence;
            m_playerDamageComputer.UpdateCriticalChance(m_heroData.CriticalChance);
            m_playerDamageComputer.UpdateCriticalDamage(m_heroData.CriticalDamage);
            
            m_playerHealth.AddRegenerationRate(ComputeRegenerationRate());
        }

        private float ComputeRegenerationRate()
        {
            return m_strengthPoints * m_strengthToRegenerationRate;
        }

        public void AddStrength(int points)
        {
            m_strengthPoints += points;
            m_strengthPoints = Mathf.Clamp(m_strengthPoints, m_heroData.BaseStrength,int.MaxValue );
        }

        public void AddAgility(int points)
        {
            m_agilityPoints += points;
            m_agilityPoints = Mathf.Clamp(m_agilityPoints, m_heroData.BaseAgility, int.MaxValue);
        }

        public void AddIntelligence(int points)
        {
            m_intelligencePoints += points;
            m_intelligencePoints = Mathf.Clamp(m_intelligencePoints, m_heroData.BaseIntelligence, int.MaxValue);
        }
    }
}

