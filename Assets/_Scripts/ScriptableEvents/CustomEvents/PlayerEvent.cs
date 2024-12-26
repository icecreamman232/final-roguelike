using System;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Player Event")]
    public class PlayerEvent : ScriptableObject
    {
        protected Action<PlayerEventType> m_listeners;
        
        public void AddListener(Action<PlayerEventType> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<PlayerEventType> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(PlayerEventType eventTypeType)
        {
            m_listeners?.Invoke(eventTypeType);
        }
    }
}