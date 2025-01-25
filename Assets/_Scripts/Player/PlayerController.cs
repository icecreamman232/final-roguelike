using SGGames.Scripts.EditorExtensions;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField][ReadOnly] private PlayerBehavior[] m_playersBehaviors;
        [SerializeField] private BoolEvent m_playerFreezeEvent;
        [SerializeField] private BoolEvent m_freezeInputEvent;
        [SerializeField] private GenericBossEvent m_genericBossEvent;

        private void Awake()
        {
            m_playerFreezeEvent.AddListener(OnPlayerFreeze);
            m_genericBossEvent.AddListener(OnGenericBossEvent);
        }

        private void Start()
        {
            m_playersBehaviors = GetComponents<PlayerBehavior>();
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

            foreach (var behaviorComponent in m_playersBehaviors)
            {
                behaviorComponent.OnPlayerFreeze(isFrozen);
            }
        }
        
        private void OnGenericBossEvent(GenericBossEventType eventType)
        {
            if (eventType == GenericBossEventType.START_FIGHT)
            {
                m_freezeInputEvent.Raise(false);
                m_playerFreezeEvent.Raise(false);
            }
        }
    }
}


