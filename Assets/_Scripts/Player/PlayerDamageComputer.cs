using System;
using System.Collections.Generic;
using SGGames.Scripts.Damages;
using SGGames.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Player
{
    public struct DamageInfo
    {
        public float MinDamage;
        public float MaxDamage;
        public float AdditionMinDamage;
        public float AdditionMaxDamage;
        public float MultiplyDamage;
        public float CriticalDamage;
    }
    
    /// <summary>
    /// Script that handle modifying damage process such as:
    /// critical strike, increase/reduce damage, multiple damage etc.
    /// </summary>
    public class PlayerDamageComputer : MonoBehaviour
    {
        [SerializeField] private float m_minDamage;
        [SerializeField] private float m_maxDamage;
        [SerializeField] private float m_additionMinDamage;
        [SerializeField] private float m_additionMaxDamage;
        [SerializeField] private float m_mutiplyDamage;
        [SerializeField] private float m_criticalChance;
        [SerializeField] private float m_criticalDamage;

        private PlayerWeaponHandler m_playerWeaponHandler;
        private DamageInfo m_damageInfo;
        private int m_dmgInfluencerCounter;
        private Dictionary<int, DamageInfluencer> m_damageInfluencerDictionary = new Dictionary<int, DamageInfluencer>();

        public float TotalMinDamage => (m_minDamage + m_additionMinDamage) * m_mutiplyDamage;
        public float TotalMaxDamage => (m_maxDamage + m_additionMaxDamage) * m_mutiplyDamage;
        public float CriticalChance => m_criticalChance;
        public float CriticalDamage => m_criticalDamage;

        private void Awake()
        {
            m_playerWeaponHandler = GetComponent<PlayerWeaponHandler>();
            m_playerWeaponHandler.OnEquipWeapon += OnEquipWeapon;
        }

        private void Start()
        {
            m_damageInfo = new DamageInfo()
            {
                MinDamage = m_minDamage,
                MaxDamage = m_maxDamage,
                AdditionMinDamage = m_additionMinDamage,
                AdditionMaxDamage = m_additionMaxDamage,
                MultiplyDamage = m_mutiplyDamage,
                CriticalDamage = GetCriticalDamage(),
            };
        }

        public void Initialize(float criticalChance, float criticalDamage)
        {
            m_criticalChance = criticalChance;
            m_criticalDamage = criticalDamage;
            AddNewDamageInfluencer(new DamageInfluencer(DamageInfluencerType.CRITICAL_CHANCE,
                100, criticalChance));
            AddNewDamageInfluencer(new DamageInfluencer(DamageInfluencerType.CRITICAL_DAMAGE,
                100, criticalDamage));
        }

        public int AddNewDamageInfluencer(DamageInfluencer damageInfluencer)
        {
            m_dmgInfluencerCounter++;
            m_damageInfluencerDictionary.Add(m_dmgInfluencerCounter, damageInfluencer);
            return m_dmgInfluencerCounter;
        }

        public void RemoveDamageInfluencer(int id)
        {
            m_damageInfluencerDictionary.Remove(id);
        }

        public static float ComputeOutputDamage(float minDamage,float additionMin, float maxDamage,float additionMax,float multiplyDamage, float criticalDamage, out bool isCritical)
        {
            var rawDamage = Mathf.Round(Random.Range(minDamage + additionMin, maxDamage + additionMax));
            var finalDamage = Mathf.Round(rawDamage * multiplyDamage * criticalDamage);
            isCritical = criticalDamage > 1;
            return finalDamage;
        }

        public DamageInfo GetDamageInfo()
        {
            ResetPreviousData();

            foreach (var damageInfluencer in m_damageInfluencerDictionary)
            {
                switch (damageInfluencer.Value.InfluencerType)
                {
                    case DamageInfluencerType.ADD_MIN_DAMAGE:
                        m_additionMinDamage += damageInfluencer.Value.GetDamage();
                        break;
                    case DamageInfluencerType.ADD_MAX_DAMAGE:
                        m_additionMaxDamage += damageInfluencer.Value.GetDamage();
                        break;
                    case DamageInfluencerType.ADD_DAMAGE:
                        m_additionMinDamage += damageInfluencer.Value.GetDamage();
                        m_additionMaxDamage += damageInfluencer.Value.GetDamage();
                        break;
                    case DamageInfluencerType.MULTIPLY_DAMAGE:
                        m_mutiplyDamage += damageInfluencer.Value.GetDamage();
                        break;
                    case DamageInfluencerType.CRITICAL_DAMAGE:
                        m_criticalDamage += damageInfluencer.Value.GetDamage();
                        break;
                    case DamageInfluencerType.CRITICAL_CHANCE:
                        m_criticalChance += damageInfluencer.Value.GetDamage();
                        
                        break;
                }
            }
            
            if (m_mutiplyDamage < 1)
            {
                m_mutiplyDamage = 1;
            }
            
            if (m_criticalDamage < 1)
            {
                m_criticalDamage = 1;
            }
            
            if (m_criticalChance < 0)
            {
                m_criticalChance = 0;
            }
            
            m_damageInfo.AdditionMinDamage = m_additionMinDamage;
            m_damageInfo.AdditionMaxDamage = m_additionMaxDamage;
            m_damageInfo.MultiplyDamage = m_mutiplyDamage;
            m_damageInfo.CriticalDamage = GetCriticalDamage();

            return m_damageInfo;
        }

        private void ResetPreviousData()
        {
            m_additionMinDamage = 0;
            m_additionMaxDamage = 0;
            m_mutiplyDamage = 1;
            m_criticalDamage = 0;
            m_criticalChance = 0;
        }
        
        private float GetCriticalDamage()
        {
            var chance = Random.Range(0, 101);
            return chance <= m_criticalChance ? m_criticalDamage : 1;
        }

        private void OnEquipWeapon(WeaponData weaponData)
        {
            m_minDamage = weaponData.MinDamage;
            m_maxDamage = weaponData.MaxDamage;
            m_damageInfo.MinDamage = m_minDamage;
            m_damageInfo.MaxDamage = m_maxDamage;
        }

        private void OnDestroy()
        {
            m_playerWeaponHandler.OnEquipWeapon -= OnEquipWeapon;
        }
    }
}
