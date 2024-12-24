using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Rooms
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private int m_doorIndex;
        [SerializeField] private SpriteRenderer m_doorSpriteRenderer;
        [SerializeField] private Sprite m_openSprite;
        [SerializeField] private GameObject m_blockDoor;
        [SerializeField] private BoxCollider2D m_triggerCollider;
        [SerializeField] private IntEvent m_playerEnterDoorEvent;
        [SerializeField] private GenericBossEvent m_genericBossEvent;

        private void Start()
        {
            m_genericBossEvent.AddListener(OnBossEvent);
        }

        private void OnDestroy()
        {
            m_genericBossEvent.RemoveListener(OnBossEvent);
        }

        private void OnBossEvent(GenericBossEventType eventType)
        {
            if (eventType == GenericBossEventType.END_FIGHT)
            {
                Open();
            }
        }

        public void Open()
        {
            m_doorSpriteRenderer.sprite = m_openSprite;
            m_blockDoor.SetActive(false);
            m_triggerCollider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                m_playerEnterDoorEvent?.Raise(m_doorIndex);
            }
        }
    }
}
