using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerBehavior[] m_playersBehaviors;
        [SerializeField] private BoolEvent m_playerFreezeEvent;
        [SerializeField] private GenericBossEvent m_genericBossEvent;

        private void Awake()
        {
            m_playerFreezeEvent.AddListener(OnPlayerFreeze);
            m_genericBossEvent.AddListener(OnGenericBossEvent);
        }

      
        private void OnDestroy()
        {
            m_playerFreezeEvent.RemoveListener(OnPlayerFreeze);
            m_genericBossEvent.RemoveListener(OnGenericBossEvent);
        }

        private void OnPlayerFreeze(bool isFrozen)
        {
            if (GameManager.Instance.IsInventoryOpened || GameManager.Instance.IsCharacterInfoOpened)
            {
                return;
            }
            for (int i = 0; i < m_playersBehaviors.Length; i++)
            {
                m_playersBehaviors[i].OnPlayerFreeze(isFrozen);
            }
        }
        
        private void OnGenericBossEvent(GenericBossEventType eventType)
        {
            if (eventType == GenericBossEventType.START_FIGHT)
            {
                m_playerFreezeEvent.Raise(false);
            }
        }
    }
}


