using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/String Event")]
    public class StringEvent : ScriptableObject
    {
        protected Action<string> m_listeners;
    
        public void AddListener(Action<string> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<string> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(string message)
        {
            m_listeners?.Invoke(message);
        }
    } 
}