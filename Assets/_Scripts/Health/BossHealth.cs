using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Healths
{
    public class BossHealth : EnemyHealth
    {
        [SerializeField] private BossHealthUpdateEvent m_BossHealthUpdateEvent;
        [SerializeField] private GenericBossEvent m_genericBossEvent;
        
        protected override void UpdateHealthBar()
        {
            m_BossHealthUpdateEvent.Raise(m_currentHealth, m_maxHealth);
        }

        protected override void Kill()
        {
            base.Kill();
            m_genericBossEvent.Raise(GenericBossEventType.END_FIGHT);
        }
    }
}
