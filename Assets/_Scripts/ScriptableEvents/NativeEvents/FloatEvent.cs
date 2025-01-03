using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Float Event")]
    public class FloatEvent : ScriptableObject
    {
        protected Action<float> m_listeners;
    
        public void AddListener(Action<float> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<float> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(float value)
        {
            m_listeners?.Invoke(value);
        }
    } 
}
