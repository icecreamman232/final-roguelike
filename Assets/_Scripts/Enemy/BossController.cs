using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BossController : EnemyController
    {
        //TODO:Change place to store boss name
        [SerializeField] private string m_bossName; //Placeholder for boss name
        [SerializeField] private GenericBossEvent m_genericBossEvent;
        [SerializeField] private StringEvent m_showBossNameEvent;
        [SerializeField] private EnemyMovement m_enemyMovement;

        private void Awake()
        {
            m_genericBossEvent.AddListener(OnBossEvent);
        }

        private void OnDestroy()
        {
            m_genericBossEvent.RemoveListener(OnBossEvent);
        }

        protected override void Start()
        {
            m_showBossNameEvent.Raise(m_bossName);
            m_genericBossEvent.Raise(GenericBossEventType.SHOW_HEALTH_BAR);
        }
        
        private void OnBossEvent(GenericBossEventType eventType)
        {
            switch (eventType)
            {
                case GenericBossEventType.SHOW_HEALTH_BAR:
                    break;
                case GenericBossEventType.START_FIGHT:
                    m_enemyMovement.ToggleAllow(true);
                    m_currentBrain.BrainActive = true;
                    break;
                case GenericBossEventType.END_FIGHT:
                    m_currentBrain.BrainActive = false;
                    break;
            }
        }
    }
}

