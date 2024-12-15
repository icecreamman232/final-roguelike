using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Game Event")]
    public class GameEvent : ScriptableObject
    {
        protected Action<Common.GameEventType> m_listeners;
        
        public void AddListener(Action<Common.GameEventType> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<Common.GameEventType> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(Common.GameEventType eventTypeType)
        {
            m_listeners?.Invoke(eventTypeType);
        }
    }
}