using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Revive Event")]
    public class UpdateReviveEvent : ScriptableObject
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

        public void Raise(int currentReviveTime)
        {
            m_listeners?.Invoke(currentReviveTime);
        }
    }
}