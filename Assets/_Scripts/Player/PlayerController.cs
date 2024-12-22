using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerBehavior[] m_playersBehaviors;
        [SerializeField] private BoolEvent m_playerFreezeEvent;

        private void Awake()
        {
            m_playerFreezeEvent.AddListener(OnPlayerFreeze);
        }

        private void OnDestroy()
        {
            m_playerFreezeEvent.RemoveListener(OnPlayerFreeze);
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
    }
}


