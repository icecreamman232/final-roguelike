using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Player Mana Update Event")]
    public class PlayerManaUpdateEvent : ScriptableObject
    {
        protected Action<float,float> m_listeners;
        
        public void AddListener(Action<float,float> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<float,float> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(float current, float max)
        {
            m_listeners?.Invoke(current,max);
        }
    } 
}