using System;
using SGGames.Scripts.Common;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class ItemPicker : MonoBehaviour, IInteractable
    {
        [SerializeField] protected InteractType m_type;
        [SerializeField] protected ItemCategory m_itemCategory;
        [SerializeField] protected ItemData m_itemData;
        [SerializeField] protected ItemPickedEvent m_itemPickedEvent;

        protected PlayerAttributeController m_playerAttributeController;
        protected PlayerInteract m_playerInteract;
        protected bool m_hasInteract;

        public void SetDataForEditor(ItemCategory itemCategory,ItemData itemData,ItemPickedEvent itemPickedEvent)
        {
            m_itemCategory = itemCategory;
            switch (m_itemCategory)
            {
                case ItemCategory.Weapon:
                    m_type = InteractType.Weapon;
                    break;
                case ItemCategory.Helmet:
                    m_type = InteractType.Helmet;
                    break;
                case ItemCategory.Armor:
                    m_type = InteractType.Armor;
                    break;
                case ItemCategory.Boots:
                    m_type = InteractType.Boots;
                    break;
                case ItemCategory.Gloves:
                    m_type = InteractType.Gloves;
                    break;
                case ItemCategory.Accessories:
                    m_type = InteractType.Accessories;
                    break;
                case ItemCategory.Charm:
                    m_type = InteractType.Charm;
                    break;
            }
            m_itemData = itemData;
            m_itemPickedEvent = itemPickedEvent;
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                m_hasInteract = true;
                if (m_playerInteract == null)
                {
                    m_playerInteract = other.GetComponent<PlayerInteract>();
                }

                if (m_playerAttributeController == null)
                {
                    m_playerAttributeController = other.GetComponent<PlayerAttributeController>();
                }
                m_playerInteract.AssignInteraction(m_type, this);
                ShowPrompt();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (m_hasInteract && other.CompareTag("Player"))
            {
                m_hasInteract = false;
                HidePrompt();
                m_playerInteract.AssignInteraction(InteractType.None, null);
            }
        }

        protected virtual void ShowPrompt()
        {
            
        }

        protected virtual void HidePrompt()
        {
            
        }
        
        public void Interact()
        {
            m_itemPickedEvent?.Raise(m_itemCategory, m_itemData, this.gameObject);
        }
    }
}

