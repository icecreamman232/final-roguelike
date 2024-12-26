using System;
using System.Collections.Generic;
using SGGames.Scripts.Damages;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Player
{
    public struct DamageInfo
    {
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
        [SerializeField] private float m_additionMinDamage;
        [SerializeField] private float m_additionMaxDamage;
        [SerializeField] private float m_mutiplyDamage;
        [SerializeField] private float m_criticalChance;
        [SerializeField] private float m_criticalDamage;

        private DamageInfo m_damageInfo;
        private int m_dmgInfluencerCounter;
        private Dictionary<int, DamageInfluencer> m_damageInfluencerDictionary = new Dictionary<int, DamageInfluencer>();


        private void Start()
        {
            m_damageInfo = new DamageInfo()
            {
                AdditionMinDamage = m_additionMinDamage,
                AdditionMaxDamage = m_additionMaxDamage,
                MultiplyDamage = m_mutiplyDamage,
                CriticalDamage = GetCriticalDamage(),
            };
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
                        if (m_mutiplyDamage < 1)
                        {
                            m_mutiplyDamage = 1;
                        }
                        break;
                    case DamageInfluencerType.CRITICAL_DAMAGE:
                        m_criticalDamage += damageInfluencer.Value.GetDamage();
                        if (m_criticalDamage < 1)
                        {
                            m_criticalDamage = 1;
                        }
                        break;
                    case DamageInfluencerType.CRITICAL_CHANCE:
                        m_criticalChance += damageInfluencer.Value.GetDamage();
                        if (m_criticalChance < 0)
                        {
                            m_criticalChance = 0;
                        }
                        break;
                }
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
            m_criticalDamage = 1;
            m_criticalChance = 0;
        }
        
        private float GetCriticalDamage()
        {
            var chance = Random.Range(0, 101);
            return chance <= m_criticalChance ? m_criticalDamage : 1;
        }
    }
}
