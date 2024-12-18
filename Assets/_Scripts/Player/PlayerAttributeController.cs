using System;
using System.Collections;
using SGGames.Scripts.Common;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Managers;
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
        [SerializeField] private PlayerWeaponHandler m_playerWeaponHandler;
        
        [Header("Events")]
        [SerializeField] private AddAttributeEvent m_addAttributeEvent;
        [Header("Data")]
        [SerializeField] private ConstantData m_constantData;
        
        public float StrengthPoints => m_strengthPoints;
        public float AgilityPoints => m_agilityPoints;
        public float IntelligencePoints => m_intelligencePoints;
        
        protected override void Start()
        {
            base.Start();
            m_addAttributeEvent.AddListener(OnChooseAttributeReward);
            StartCoroutine(InitializeAttributes());
        }

        private void OnDestroy()
        {
            m_addAttributeEvent.RemoveListener(OnChooseAttributeReward);
        }
        
        private IEnumerator InitializeAttributes()
        {
            m_strengthPoints = m_heroData.BaseStrength;
            m_agilityPoints = m_heroData.BaseAgility;
            m_intelligencePoints = m_heroData.BaseIntelligence;
            m_playerDamageComputer.UpdateCriticalChance(m_heroData.CriticalChance);
            m_playerDamageComputer.UpdateCriticalDamage(m_heroData.CriticalDamage);
            
            m_playerHealth.Initialize(ComputeMaxHealth());
            m_playerHealth.AddRegenerationRate(ComputeRegenerationRate());

            yield return new WaitUntil(() =>
                m_playerWeaponHandler != null && m_playerWeaponHandler.IsWeaponInitialized);
            m_playerWeaponHandler.ApplyAttackSpeedOnCurrentWeapon(ComputeDelayBetweenAttacks(m_playerWeaponHandler.BaseAtkTime));
        }

        private float ComputeMaxHealth()
        {
            return m_strengthPoints * m_constantData.C_STR_TO_HEALTH;
        }
        private float ComputeRegenerationRate()
        {
            return m_strengthPoints * m_constantData.C_STR_TO_REGENERATE;
        }

        private float ComputeAtkSpeed()
        {
            var atkSpd = m_agilityPoints * m_constantData.C_AGI_TO_ATK_SPD;
            atkSpd = Mathf.Clamp(atkSpd, 0, m_constantData.C_MAX_ATK_SPD);
            
            return atkSpd;
        }

        public float ComputeAtkRate(float baseAtkTime)
        {
            return ComputeAtkSpeed()/(m_constantData.C_ATK_SPD_TO_ATK_RATE * baseAtkTime);
        }

        public float ComputeDelayBetweenAttacks(float baseAtkTime)
        {
            return 1 / ComputeAtkRate(baseAtkTime);
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
            
            m_playerWeaponHandler.ApplyAttackSpeedOnCurrentWeapon(ComputeDelayBetweenAttacks(m_playerWeaponHandler.BaseAtkTime));
        }

        public void AddIntelligence(float points)
        {
            m_intelligencePoints += points;
            m_intelligencePoints = Mathf.Clamp(m_intelligencePoints, m_heroData.BaseIntelligence, int.MaxValue);
        }

        private int GetRewardAmount(UpgradeAttributeRate rate)
        {
            switch (rate)
            {
                case UpgradeAttributeRate.Common:
                    return AttributeRewardController.CommonRewardPoint;
                case UpgradeAttributeRate.Uncommon:
                    return AttributeRewardController.UncommonRewardPoint;
                case UpgradeAttributeRate.Rare:
                    return AttributeRewardController.RareRewardPoint;
                case UpgradeAttributeRate.Legendary:
                    return AttributeRewardController.LegendaryRewardPoint;
            }
            return AttributeRewardController.CommonRewardPoint;
        }
        
        private void OnChooseAttributeReward((UpgradeAttributeRate rate, AttributeType type) reward)
        {
            var amount= GetRewardAmount(reward.rate);
            switch (reward.type)
            {
                case AttributeType.Strength:
                    AddStrength(amount);
                    break;
                case AttributeType.Agility:
                    AddAgility(amount);
                    break;
                case AttributeType.Intelligence:
                    AddIntelligence(amount);
                    break;
            }
        }
    }
}

