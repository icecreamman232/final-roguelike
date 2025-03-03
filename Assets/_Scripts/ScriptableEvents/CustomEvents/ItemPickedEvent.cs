using System;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Item Picked Event")]
    public class ItemPickedEvent : ScriptableObject
    {
        protected Action<ItemCategory,ItemData, GameObject> m_listeners;
        
        public void AddListener(Action<ItemCategory,ItemData, GameObject> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<ItemCategory,ItemData, GameObject> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(ItemCategory category, ItemData data, GameObject picker)
        {
            m_listeners?.Invoke(category,data,picker);
        }
    }
}

