using UnityEngine;

namespace SGGames.Scripts.Player
{
    /// <summary>
    /// Script that handle modifying damage process such as:
    /// critical strike, increase/reduce damage, multiple damage etc.
    /// </summary>
    public class PlayerDamageComputer : MonoBehaviour
    {
        [SerializeField] private float m_additionDamage;
        [SerializeField] private float m_mutiplyDamage;
        [SerializeField] private float m_criticalChance;
        [SerializeField] private float m_criticalDamage;

        public (float additionDamage, float multiplyDamage, float criticalDamage) 
            GetDamageInfo => (m_additionDamage, m_mutiplyDamage, GetCriticalDamage());

        public void AddCriticalChance(float chance)
        {
            m_criticalChance += chance;
        }

        public void AddCriticalDamage(float criticalDamage)
        {
            m_criticalDamage += criticalDamage;
        }

        public void UpdateAdditionDamage(float additionDamage)
        {
            m_additionDamage += additionDamage;
        }

        public void UpdateMultiplyDamage(float multiplyDamage)
        {
            m_mutiplyDamage += multiplyDamage;
        }
        
        private float GetCriticalDamage()
        {
            var chance = Random.Range(0, 101);
            if (chance <= m_criticalChance)
            {
                return m_criticalDamage;
            }
            else
            {
                return 1;
            }
        }
    }
}
