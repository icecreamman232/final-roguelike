using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Stamina Update")]
    public class StaminaUpdateEvent : ScriptableObject
    {
        protected Action<int,int> m_listeners;
        
        public void AddListener(Action<int,int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<int,int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(int current, int max)
        {
            m_listeners?.Invoke(current, max);
        }
    }
}