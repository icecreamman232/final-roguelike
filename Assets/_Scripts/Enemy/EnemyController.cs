using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private bool m_isSubEnemy;
        [SerializeField] private int m_subEnemyNumber;
        [SerializeField] protected EnemyBrain m_currentBrain;
        [SerializeField] protected GameEvent m_gameEvent;
        
        public EnemyBrain CurrentBrain => m_currentBrain;

        protected virtual void Start()
        {
            if (!m_isSubEnemy)
            {
                //Try to find how many sub enemy will be spawned on enemy death
                var numberSpawnOnDeathComponent = GetComponentsInChildren<SpawnOnDeath>();
                if (numberSpawnOnDeathComponent.Length > 0)
                {
                    m_subEnemyNumber = numberSpawnOnDeathComponent.Length;
                }
                LevelManager.Instance.AddEnemyNumberInRoom(m_subEnemyNumber + 1); //Include this enemy self
            }
            m_gameEvent.AddListener(OnGameEvent);
        }

        private void OnDestroy()
        {
            m_gameEvent.RemoveListener(OnGameEvent);
        }

        private void OnGameEvent(GameEventType eventType)
        {
            if (eventType == GameEventType.ENTER_THE_ROOM)
            {
                m_currentBrain.BrainActive = true;
            }
        }

        public void SetActiveBrain(EnemyBrain newBrain)
        {
            m_currentBrain = newBrain;
        }
    }
}

