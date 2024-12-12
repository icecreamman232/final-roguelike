using UnityEngine;

namespace SGGames.Scripts.Rooms
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_doorSpriteRenderer;
        [SerializeField] private Sprite m_openSprite;
        [SerializeField] private GameObject m_blockDoor;
        [SerializeField] private BoxCollider2D m_triggerCollider;
        
        public void Open()
        {
            m_doorSpriteRenderer.sprite = m_openSprite;
            m_blockDoor.SetActive(false);
            m_triggerCollider.enabled = true;
        }
    }
}
