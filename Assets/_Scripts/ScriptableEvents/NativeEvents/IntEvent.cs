using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Int Event")]
    public class IntEvent : ScriptableObject
    {
        protected Action<int> m_listeners;
    
        public void AddListener(Action<int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(int value)
        {
            m_listeners?.Invoke(value);
        }
    } 
}
