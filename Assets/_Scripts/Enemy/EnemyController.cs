using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using SGGames.Scripts.Modifiers;
using SGGames.Scripts.StatusEffects;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private bool m_isSubEnemy;
        [SerializeField] private int m_subEnemyNumber;
        [SerializeField] protected EnemyBrain m_currentBrain;
        [SerializeField] protected GameEvent m_gameEvent;

        private EnemyModifierHandler m_modifierHandler;
        private EnemyStatusEffectHandler m_statusEffectHandler;
        public EnemyModifierHandler ModifierHandler => m_modifierHandler;
        public EnemyStatusEffectHandler StatusEffectHandler => m_statusEffectHandler;
        public EnemyBrain CurrentBrain => m_currentBrain;

        protected virtual void Start()
        {
            if (!m_isSubEnemy)
            {
                //Try to find how many sub enemy will be spawned on enemy death
                var numberSpawnOnDeathComponent = GetComponentsInChildren<SpawnOnDeath>();

                for (int i = 0; i < numberSpawnOnDeathComponent.Length; i++)
                {
                    if (numberSpawnOnDeathComponent[i] is SpawnProjectileOnDeath)
                    {
                        continue;
                    }
                    m_subEnemyNumber++;
                }
                LevelManager.Instance.AddEnemyNumberInRoom(m_subEnemyNumber + 1); //Include this enemy self
            }

            m_modifierHandler = GetComponentInChildren<EnemyModifierHandler>();
            if (m_modifierHandler == null)
            {
                Debug.LogError($"EnemyModifierHandler not found on {this.gameObject.name}");
            }

            m_statusEffectHandler = GetComponentInChildren<EnemyStatusEffectHandler>();
            if (m_statusEffectHandler == null)
            {
                Debug.LogError($"StatusEffectHandler not found on {this.gameObject.name}");
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

