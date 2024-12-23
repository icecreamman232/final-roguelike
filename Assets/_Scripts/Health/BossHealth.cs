using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class BossHealth : EnemyHealth
    {
        [SerializeField] private BossHealthUpdateEvent m_BossHealthUpdateEvent;
        
        protected override void UpdateHealthBar()
        {
            m_BossHealthUpdateEvent.Raise(m_currentHealth, m_maxHealth);
        }
    }
}
