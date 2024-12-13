using System;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerAttributeController : PlayerBehavior
    {
        [Header("Base Attributes")]
        [SerializeField] private HeroData m_heroData;
        [SerializeField] private float m_strengthPoints;
        [SerializeField] private float m_agilityPoints;
        [SerializeField] private float m_intelligencePoints;
        
        [Header("Components")]
        [SerializeField] private PlayerHealth m_playerHealth;
        [SerializeField] private PlayerDamageComputer m_playerDamageComputer;
        
        [Header("Events")]
        [SerializeField] private IntEvent m_playerLevelUpEvent;
        
        private readonly float m_strengthToRegenerationRate = 0.05f;
        private readonly float m_strengthToHealth = 30;

        public float StrengthPoints => m_strengthPoints;
        public float AgilityPoints => m_agilityPoints;
        public float IntelligencePoints => m_intelligencePoints;
        
        protected override void Start()
        {
            base.Start();
            m_playerLevelUpEvent.AddListener(OnPlayerLevelUp);
            InitializeAttributes();
        }

        private void OnDestroy()
        {
            m_playerLevelUpEvent.RemoveListener(OnPlayerLevelUp);
        }

        private void InitializeAttributes()
        {
            m_strengthPoints = m_heroData.BaseStrength;
            m_agilityPoints = m_heroData.BaseAgility;
            m_intelligencePoints = m_heroData.BaseIntelligence;
            m_playerDamageComputer.UpdateCriticalChance(m_heroData.CriticalChance);
            m_playerDamageComputer.UpdateCriticalDamage(m_heroData.CriticalDamage);
            
            m_playerHealth.Initialize(ComputeMaxHealth());
            m_playerHealth.AddRegenerationRate(ComputeRegenerationRate());
        }

        private float ComputeMaxHealth()
        {
            return m_strengthPoints * m_strengthToHealth;
        }
        private float ComputeRegenerationRate()
        {
            return m_strengthPoints * m_strengthToRegenerationRate;
        }

        public void AddStrength(float points)
        {
            m_strengthPoints += points;
            m_strengthPoints = Mathf.Clamp(m_strengthPoints, m_heroData.BaseStrength,int.MaxValue );
            
            m_playerHealth.OverrideMaxHealth(ComputeMaxHealth());
            m_playerHealth.AddRegenerationRate(ComputeRegenerationRate());
        }

        public void AddAgility(float points)
        {
            m_agilityPoints += points;
            m_agilityPoints = Mathf.Clamp(m_agilityPoints, m_heroData.BaseAgility, int.MaxValue);
        }

        public void AddIntelligence(float points)
        {
            m_intelligencePoints += points;
            m_intelligencePoints = Mathf.Clamp(m_intelligencePoints, m_heroData.BaseIntelligence, int.MaxValue);
        }
        
        private void OnPlayerLevelUp(int level)
        {
            AddStrength(m_heroData.GrowthStrength);
            AddAgility(m_heroData.GrowthAgility);
            AddIntelligence(m_heroData.GrowthIntelligence);
        }
    }
}

