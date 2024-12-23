using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    public enum GenericBossEventType
    {
        SHOW_HEALTH_BAR,
        START_FIGHT,
        END_FIGHT,
    }
    
    [CreateAssetMenu(menuName = "SGGames/Event/Generic Boss Event")]
    public class GenericBossEvent : ScriptableObject
    {
        protected Action<GenericBossEventType> m_listeners;
        
        public void AddListener(Action<GenericBossEventType> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<GenericBossEventType> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(GenericBossEventType eventType)
        {
            m_listeners?.Invoke(eventType);
        }
    }
}