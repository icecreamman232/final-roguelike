using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class ItemPicker : Pickable
    {
        [SerializeField] private ItemCategory m_itemCategory;
        [SerializeField] private ItemData m_itemData;
        [SerializeField] private ItemPickedEvent m_itemPickedEvent;
        
        protected override void Picked()
        {
            m_itemPickedEvent?.Raise(m_itemCategory, m_itemData);
            base.Picked();
        }
    }
}

