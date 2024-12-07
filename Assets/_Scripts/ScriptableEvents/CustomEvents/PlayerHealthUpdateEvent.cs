using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Player Health Update Event")]
    public class PlayerHealthUpdateEvent : ScriptableObject
    {
        protected Action<float,float,GameObject> m_listeners;
        
        public void AddListener(Action<float,float,GameObject> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<float,float,GameObject> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(float current, float max, GameObject source)
        {
            m_listeners?.Invoke(current,max,source);
        }
    } 
}