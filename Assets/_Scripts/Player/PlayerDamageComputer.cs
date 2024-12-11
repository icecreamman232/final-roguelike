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
        
        public float AdditionDamage => m_additionDamage;
        public float MultiplyDamage => m_mutiplyDamage;

        public void UpdateAdditionDamage(float additionDamage)
        {
            m_additionDamage += additionDamage;
        }

        public void UpdateMultiplyDamage(float multiplyDamage)
        {
            m_mutiplyDamage += multiplyDamage;
        }
    }
}
