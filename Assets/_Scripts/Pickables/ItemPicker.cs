using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class ItemPicker : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractType m_type;
        [SerializeField] private ItemCategory m_itemCategory;
        [SerializeField] private ItemData m_itemData;
        [SerializeField] private ItemPickedEvent m_itemPickedEvent;
        
        private PlayerInteract m_playerInteract;
        private bool m_hasInteract;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                m_hasInteract = true;
                if (m_playerInteract == null)
                {
                    m_playerInteract = other.GetComponent<PlayerInteract>();
                }
                m_playerInteract.AssignInteraction(m_type, this);
                ShowPrompt();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (m_hasInteract && other.CompareTag("Player"))
            {
                m_hasInteract = false;
                m_playerInteract.AssignInteraction(InteractType.None, null);
                HidePrompt();
            }
        }

        private void ShowPrompt()
        {
            
        }

        private void HidePrompt()
        {
            
        }
        
        public void Interact()
        {
            m_itemPickedEvent?.Raise(m_itemCategory, m_itemData);
            Destroy(this.gameObject);
        }
    }
}

